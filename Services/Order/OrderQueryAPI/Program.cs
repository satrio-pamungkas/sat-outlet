using Microsoft.EntityFrameworkCore;
using OrderQueryAPI.Consumers;
using OrderQueryAPI.Data;
using OrderQueryAPI.Handlers;
using OrderQueryAPI.Models;
using OrderQueryAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
var dbString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(dbString));
builder.Services.AddHostedService<OrderTopicConsumer>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderHandler, OrderHandler>();
builder.Services.AddScoped<IProductHandler, ProductHandler>();

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