using AutoMapper;
using Discount.Domain.Repository.Interfaces;
using Discount.Domain.Repository.Repositories;
using Discount.Grpc.Configuration;
using Discount.Grpc.Extensions;
using Discount.Grpc.Services;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<ApplicationSettings>();


IMapper mapper = MapperInitializer.GetConfiguredMapper();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(settings.DbSettings.ConnectionString));
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddSingleton(mapper);
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<LoggerInterceptor>();
});

var app = builder.Build();
app.MigrateDatabase<Program>(settings.DbSettings);

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
