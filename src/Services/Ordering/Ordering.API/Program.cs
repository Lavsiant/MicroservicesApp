using MongoDB.Driver;
using Ordering.API.Common;
using Ordering.API.Configuration;
using Ordering.API.HostExtensions;
using Ordering.Application.Handlers;
using Ordering.Application.Interfaces;
using Ordering.Application.PubSub;
using Ordering.Application.Services;
using Ordering.Domain.Aggregates.OrderModule;
using Ordering.Domain.Core.Interfaces;
using Ordering.Domain.Persistence;
using Ordering.Domain.Persistence.EventStore;
using Ordering.Domain.ReadModel.Interfaces;
using Ordering.Domain.ReadModel.Repositories;
using Ordering.Infrastructure.Interfaces.EventStore;
using Ordering.Infrastructure.Repositories;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = builder.Configuration.Get<ApplicationSettings>();
var mssqlSettings = settings.SQLEventStoreSettings;


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(x => new MongoClient(settings.MongoDbSettings.ConnectionString));
builder.Services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(settings.MongoDbSettings.DatabaseName));
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(ConnectionStringBuilder.BuildSqlConnectionString(
    mssqlSettings.Server, mssqlSettings.Database, mssqlSettings.UserId, mssqlSettings.Password)));

builder.Services.AddTransient<IOrderRepository, OrderRepository>(); 
builder.Services.AddTransient<IEventStore, EventStore>();
builder.Services.AddTransient<IAggregateRepository<OrderAggregate>, AggregateRepository<OrderAggregate>>();
builder.Services.AddTransient<IDomainEventHandler<OrderCreatedEvent>, OrderUpdater>();
builder.Services.AddTransient<IDomainEventHandler<OrderCancelledEvent>, OrderUpdater>();
builder.Services.AddTransient<IDomainEventHandler<OrderUpdatedEvent>, OrderUpdater>();
builder.Services.AddTransient<IOrderWriter, OrderWriter>();
builder.Services.AddScoped<IOrderReader, OrderReader>();
//builder.Services.AddScoped<IScopedDomainEventSubscriber, ScopedDomainEventPubSub>();
builder.Services.AddScoped<IScopedDomainEventPubSub, ScopedDomainEventPubSub>();

builder.Services.AddSingleton(MapperInitializer.GetConfiguredMapper());

 var app = builder.Build();

app.CreateDatabase(mssqlSettings);
app.MigrateDatabase();// settings.SQLEventStoreSettings.ConnectionString);

app.UseDeveloperExceptionPage();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
