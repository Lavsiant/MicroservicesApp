using Basket.API.Configuration;
using Basket.API.Repositories.Interfaces;
using Basket.Domain.Repository.Repositories;
using Basket.Services.Implementations;
using Basket.Services.Interfaces;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<ApplicationSettings>();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = settings.CacheSettings.ConnectionString;
});

// Services
builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<IDiscountService, DiscountGrpcService>();

// Providers
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt => opt.Address = new Uri(settings.GrpcSettings.DiscountUrl));
builder.Services.AddTransient<IBasketRepository, BasketRepository>();

builder.Services.AddSingleton(MapperInitializer.GetConfiguredMapper());

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
