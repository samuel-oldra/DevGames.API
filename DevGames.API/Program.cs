using DevGames.API.Mappers;
using DevGames.API.Persistence;
using DevGames.API.Persistence.Repositories;
using DevGames.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// PARA ACESSO AO BANCO EM MEMÓRIA
// builder.Services.AddDbContext<DevGamesContext>(o => o.UseInMemoryDatabase("DevGamesDb"));

// PARA ACESSO AO SQLite
var connectionString = builder.Configuration.GetConnectionString("DevGamesCs");
builder.Services.AddDbContext<DevGamesContext>(o => o.UseSqlite(connectionString));

// Injeção de Dependência
// Tipos: Transient, Scoped, Singleton
// Padrão Repository
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevGames.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Samuel B. Oldra",
            Email = "samuel.oldra@gmail.com",
            Url = new Uri("https://github.com/samuel-oldra")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    o.IncludeXmlComments(xmlPath);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(BoardMapper));

// Serilog
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()

        // PARA LOG NO SQL Server
        .WriteTo.SQLite(Environment.CurrentDirectory + @"\Data\dados.db")

        // PARA LOG NO CONSOLE
        .WriteTo.Console()

        .CreateLogger();
}).UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
// INFO: Swagger visível só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.RoutePrefix = string.Empty;
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "DevGames.API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();