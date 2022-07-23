using AutoMapper;
using Coupon.Grpc.Configuration;
using Coupon.Grpc.Extensions;
using Coupon.Grpc.Services;
using Discount.Domain.Repository.Interfaces;
using Discount.Domain.Repository.Repositories;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<ApplicationSettings>();


IMapper mapper = MapperInitializer.GetConfiguredMapper();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(settings.DbSettings.ConnectionString));
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<LoggerInterceptor>();
});
builder.Services.AddSingleton(mapper);

var app = builder.Build();
app.MigrateDatabase<Program>(settings.DbSettings);

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
