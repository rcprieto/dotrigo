using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
	{
		// services.AddDbContext<DataContext>(opt =>
		// {
		// 	opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
		// });


		services.AddDbContext<DbContext>(options => options
			.UseMySql(config.GetConnectionString("DbConnectionString"),
			new MySqlServerVersion(new Version(8, 0, 19)),
			b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schemaName, objectName) => objectName)
			)
			.EnableSensitiveDataLogging()
			.EnableDetailedErrors());

		services.AddCors();
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddScoped<DbContext, DbContext>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		services.AddScoped<DbContext>();






		return services;
	}

}
