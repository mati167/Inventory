using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Repositories;
using NLog;
using NLog.Web;
using Peliculas.Core.Interfaces;
using Peliculas.Core.Services;
using System.Diagnostics;
using ILogger = NLog.ILogger;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


ILogger _log = LogManager.GetCurrentClassLogger();

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IFilmService, FilmService>();
builder.Services.AddTransient<IFilmRepository, filmRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//var foldernamePublish = Directory.GetCurrentDirectory().Substring(Directory.GetCurrentDirectory().LastIndexOf('\\') + 1);


//if (Debugger.IsAttached)
//{
//    logger.Info(foldernamePublish);
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Arquetipo API V1");
//        c.DefaultModelsExpandDepth(-1);
//    });
//}
//else
//{
//    logger.Info(foldernamePublish);
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint($"/{foldernamePublish}/swagger/v1/swagger.json", $"{foldernamePublish} V1");
//        c.DefaultModelsExpandDepth(-1);
//    });
//}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
