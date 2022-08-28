using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductApi.Db;
using ProductApi.Models;
using ProductApi.Repositories.Implementations;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Implementations;
using ProductApi.Services.Interfaces;
using ProductApi.Validation;
using System.Reflection;
using System.Web.WebPages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(c =>
    {
        c.ImplicitlyValidateChildProperties = true;
        // using the automatic register method to register the validators
        c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

    });

// builder.Services.AddTransient<IValidator<Product>, ProductValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<ProductContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

builder.Services.AddHttpClient();

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
