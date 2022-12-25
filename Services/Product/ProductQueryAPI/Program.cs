using Microsoft.EntityFrameworkCore;
using ProductQueryAPI.Consumers;
using ProductQueryAPI.Data;
using ProductQueryAPI.Handlers;
using ProductQueryAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
var dbString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(dbString));
builder.Services.AddHostedService<ProductTopicConsumer>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInsertProduct, InsertProduct>();

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