using Catalog.API.Models;
using Catalog.Repository.Interfaces;
using Catalog.Repository.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


var settings = builder.Configuration.Get<ApplicationSettings>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddSingleton(x => new MongoClient(settings.MongoDbSettings.ConnectionString));
builder.Services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(settings.MongoDbSettings.DatabaseName));
builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
