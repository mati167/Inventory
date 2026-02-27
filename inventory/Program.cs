using Inventory.Infrastructure.Repositories;
using NLog;
using NLog.Web;
using Peliculas.Core.Services;
using System.Diagnostics;
using ILogger = NLog.ILogger;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Inventory.Core.Interfaces.Services;
using Inventory.Core.Interfaces.Repository;
using Inventory.Core.Services;

// Crear carpeta de logs si no existe
string logPath = @"C:\LOGS";
if (!Directory.Exists(logPath))
{
    try
    {
        Directory.CreateDirectory(logPath);
        Console.WriteLine($"✅ Carpeta de logs creada: {logPath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ No se pudo crear la carpeta de logs: {ex.Message}");
    }
}

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add NLog to DI
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
               options.JsonSerializerOptions.WriteIndented = true;
           });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddTransient<IFilmService, FilmService>();
    builder.Services.AddTransient<IFilmRepository, filmRepository>();
    builder.Services.AddTransient<IpersonService, personService>();
    builder.Services.AddTransient<IpersonRepository, personRepository>();
    builder.Services.AddTransient<ICountryService, countryService>();
    builder.Services.AddTransient<ICountryRepository, countryRepository>();
    builder.Services.AddTransient<IGenreService, genreService>();
    builder.Services.AddTransient<IGenreRepository, genreRepository>();

    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
    }
    app.UseSwagger();
    app.UseSwaggerUI();

    var foldernamePublish = Directory.GetCurrentDirectory().Substring(Directory.GetCurrentDirectory().LastIndexOf('\\') + 1);

    logger.Info($"═══════════════════════════════════════════════════════");
    logger.Info($"🚀 Aplicación iniciada: {foldernamePublish}");
    logger.Info($"📁 Carpeta de logs: {logPath}");
    logger.Info($"🔗 URL: http://localhost:4351/swagger/index.html");
    logger.Info($"═══════════════════════════════════════════════════════");

    if (Debugger.IsAttached)
    {
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Arquetipo API V1");
            c.DefaultModelsExpandDepth(-1);
        });
    }
    else
    {
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/{foldernamePublish}/swagger/v1/swagger.json", $"{foldernamePublish} V1");
            c.DefaultModelsExpandDepth(-1);
        });
    }

    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "❌ Se produjo un error durante la inicialización de la aplicación");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
