using DevGames.API.Mappers;
using DevGames.API.Persistence;
using DevGames.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// PARA ACESSO AO BANCO EM MEM�RIA
builder.Services.AddDbContext<DevGamesContext>(o => o.UseInMemoryDatabase("DevGamesDb"));

// PARA ACESSO AO SQL Server
// var connectionString = builder.Configuration.GetConnectionString("DevGamesCs");
// builder.Services.AddDbContext<DevGamesContext>(o => o.UseSqlServer(connectionString));

// Configura AutoMapper
builder.Services.AddAutoMapper(typeof(BoardMapper));

// Padr�o Repository
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevGames API",
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();