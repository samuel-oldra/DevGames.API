using DevGames.API.Mappers;
using DevGames.API.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 01.16.00
//builder.Services.AddDbContext<DevGamesContext>(o => o.UseInMemoryDatabase("DevGamesCs"));

var connectionString = builder.Configuration.GetConnectionString("DevGamesDb");
builder.Services.AddDbContext<DevGamesContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(BoardMapper)); // Configura AutoMapper
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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