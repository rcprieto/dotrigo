using API.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace API.Data.Context
{
    public class DoTrigoDbContext : DbContext
    {
        public DoTrigoDbContext(DbContextOptions<DoTrigoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a precisão dos campos decimais
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(10, 2);

            // Configura os ENUMs para serem salvos como strings (varchar) no banco
            // Isso é crucial para corresponder ao ENUM do MySQL
            modelBuilder.Entity<Produto>()
                .Property(p => p.Categoria)
                .HasConversion<string>();

            // Define valores padrão
            modelBuilder.Entity<Produto>()
               .Property(p => p.ProntaEntrega)
               .HasDefaultValue(false);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Ativo)
                .HasDefaultValue(true);


            // --- Configuração da Tabela 'pedidos' ---

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ValorTotal)
                .HasPrecision(10, 2);

            // --- Configuração da Tabela 'pedido_itens' ---

            modelBuilder.Entity<PedidoItem>()
                .Property(p => p.PrecoUnitario)
                .HasPrecision(10, 2);

            // Configura as Relações (Foreign Keys) e Comportamento de Exclusão

            // Relação Pedido (1) -> PedidoItens (N)
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens) // Um Pedido tem muitos Itens
                .WithOne(pi => pi.Pedido) // Um Item pertence a um Pedido
                .HasForeignKey(pi => pi.PedidoId)
                .OnDelete(DeleteBehavior.Cascade); // Se deletar o Pedido, deleta os Itens

            // Relação Produto (1) -> PedidoItens (N)
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.PedidoItens) // Um Produto pode estar em muitos Itens
                .WithOne(pi => pi.Produto) // Um Item aponta para um Produto
                .HasForeignKey(pi => pi.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict); // NÃO deixa deletar um Produto se ele estiver em um Pedido


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .AddSingleton<ISqlGenerationHelper, CustomMySqlSqlGenerationHelper>()
                .AddScoped(
                    s => LoggerFactory.Create(
                        b => b
                            .AddFilter(level => level >= LogLevel.Information)))
                .BuildServiceProvider();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseInternalServiceProvider(serviceProvider)
                    .UseMySql("Server=18.231.38.236; Database=ws_dotrigo; Uid=dotrigo; Pwd=9xaGXa$VnAdhQK3@dotrigo; Convert Zero Datetime=True;Port=3306;SslMode=None",
                    new MySqlServerVersion(new Version(8, 0, 19)), mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5, // Número máximo de retentativas
                            maxRetryDelay: TimeSpan.FromSeconds(30), // Atraso máximo entre as retentativas
                            errorNumbersToAdd: null // Números de erro adicionais para retentativa
                        );
                        mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Translate, (schemaName, objectName) => objectName);
                    })
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();


                //b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schemaName, objectName) => objectName))
                //.EnableSensitiveDataLogging()
                //.EnableDetailedErrors();


            }
            else
            {
                optionsBuilder
                    .UseInternalServiceProvider(serviceProvider);
            }
        }


        public DbSet<Enumerador> Enumeradors { get; set; }
        public DbSet<Referencia> Referencias { get; set; }



    }
}