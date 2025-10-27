using System.Globalization;
using API;
using API.Domain.Auxiliar;
using API.Domain.Auxiliares;
using API.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;


var builder = WebApplication.CreateBuilder(args);


var connect = builder.Configuration.GetSection("ConnectionStrings");
var appSettings = builder.Configuration.GetSection("AppSetting");



// builder.Services.Configure<ConnStringsHelper>(connect);
// builder.Services.AddSingleton(connect.Get<ConnStringsHelper>());

builder.Services.Configure<ConnStringsHelper>(connect);
builder.Services.AddSingleton(connect.Get<ConnStringsHelper>());


builder.Services.AddApplicationServices(builder.Configuration);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson();

#region AWS S3

#endregion


builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("pt-BR")
    };
    var supportedUICultures = new[]
    {
                    new CultureInfo("en"),
                    new CultureInfo("es"),
                    new CultureInfo("fr"),
                    new CultureInfo("pt-BR"),
                    new CultureInfo("pt")
                };

    opts.DefaultRequestCulture = new RequestCulture("pt-BR");
    opts.SetDefaultCulture("pt-BR");
    opts.SupportedCultures = supportedCultures;
    opts.SupportedUICultures = supportedCultures;
    var provider = new RouteDataRequestCultureProvider { RouteDataStringKey = "lang", UIRouteDataStringKey = "lang", Options = opts };
    opts.RequestCultureProviders = new[] { provider };
});


#region Criptografia
CriptografiaQueryString cripto = new CriptografiaQueryString("0ry0nr1b");
builder.Services.AddSingleton(cripto);

#endregion


var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors(x =>
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials() //SignaIR precisa
    .WithOrigins(["https://localhost:4200",]));

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Erro interno no servidor." + ex.Message);
    }
});
app.UseRouting();
app.UseAuthentication(); //Vc tem um token valido
app.UseAuthorization(); //Vc tem um token mas o que vc pode fazer

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

//Para rodar o angular dentro do wwwroot
app.MapFallbackToController("Index", "Fallback");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});


//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//try
//{

//}
//catch (Exception ex)
//{
//    var logger = services.GetService<ILogger<Program>>();
//    if (logger != null)
//        logger.LogError(ex, "Erro durante o migration");

//}

app.Run();
