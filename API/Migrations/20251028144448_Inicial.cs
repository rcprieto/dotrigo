using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ws_dotrigo");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_referencia",
                schema: "ws_dotrigo",
                columns: table => new
                {
                    ref_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ref_nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ref_status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_referencia", x => x.ref_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_enum",
                schema: "ws_dotrigo",
                columns: table => new
                {
                    enum_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    enum_nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ref_id = table.Column<int>(type: "int", nullable: true),
                    enum_status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    enum_ordem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_enum", x => x.enum_id);
                    table.ForeignKey(
                        name: "FK_tb_enum_tb_referencia_ref_id",
                        column: x => x.ref_id,
                        principalSchema: "ws_dotrigo",
                        principalTable: "tb_referencia",
                        principalColumn: "ref_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_pedido",
                schema: "ws_dotrigo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteNome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClienteTelefone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comentarios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    enum_id = table.Column<int>(type: "int", nullable: true),
                    DataPedido = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_pedido_tb_enum_enum_id",
                        column: x => x.enum_id,
                        principalSchema: "ws_dotrigo",
                        principalTable: "tb_enum",
                        principalColumn: "enum_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_produto",
                schema: "ws_dotrigo",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enum_cat_id = table.Column<int>(type: "int", nullable: true),
                    FotoUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tamanho = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Ex: \"Pequena\", \"Média\", \"Grande\", \"Unidade\"")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Peso = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Ex: \"300g\", \"500g\"")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TempoPreparo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Ex: \"20 min\", \"Pronto\"")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProntaEntrega = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "1 = Sim, 0 = Não"),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true, comment: "1 = Visível no menu, 0 = Oculto"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_produto", x => x.prod_id);
                    table.ForeignKey(
                        name: "FK_tb_produto_tb_enum_enum_cat_id",
                        column: x => x.enum_cat_id,
                        principalSchema: "ws_dotrigo",
                        principalTable: "tb_enum",
                        principalColumn: "enum_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_pedido_item",
                schema: "ws_dotrigo",
                columns: table => new
                {
                    ref_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false, comment: "Preço do produto no momento da compra"),
                    NomeProduto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome do produto no momento da compra")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedido_item", x => x.ref_id);
                    table.ForeignKey(
                        name: "FK_tb_pedido_item_tb_pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalSchema: "ws_dotrigo",
                        principalTable: "tb_pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_pedido_item_tb_produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "ws_dotrigo",
                        principalTable: "tb_produto",
                        principalColumn: "prod_id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Itens individuais de cada pedido.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_enum_ref_id",
                schema: "ws_dotrigo",
                table: "tb_enum",
                column: "ref_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedido_enum_id",
                schema: "ws_dotrigo",
                table: "tb_pedido",
                column: "enum_id");

            migrationBuilder.CreateIndex(
                name: "idx_pedidoId",
                schema: "ws_dotrigo",
                table: "tb_pedido_item",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "idx_produtoId",
                schema: "ws_dotrigo",
                table: "tb_pedido_item",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_produto_enum_cat_id",
                schema: "ws_dotrigo",
                table: "tb_produto",
                column: "enum_cat_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_pedido_item",
                schema: "ws_dotrigo");

            migrationBuilder.DropTable(
                name: "tb_pedido",
                schema: "ws_dotrigo");

            migrationBuilder.DropTable(
                name: "tb_produto",
                schema: "ws_dotrigo");

            migrationBuilder.DropTable(
                name: "tb_enum",
                schema: "ws_dotrigo");

            migrationBuilder.DropTable(
                name: "tb_referencia",
                schema: "ws_dotrigo");
        }
    }
}
