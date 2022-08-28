using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductStockApi.Db;
using ProductStockApi.Repositories.Implementations;
using ProductStockApi.Repositories.Interfaces;
using ProductStockApi.Services.Implementations;
using ProductStockApi.Services.Interfaces;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddControllers()
    .AddFluentValidation(c =>
    {
        c.ImplicitlyValidateChildProperties = true;
        // using the automatic register method to register the validators
        c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

    });

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager config = builder.Configuration;

builder.Services.AddDbContext<ProductStockContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductStockRepository, ProductStockRepository>();
builder.Services.AddScoped<IProductStockService, ProductStockService>();

builder.Services.AddHttpClient();

builder.Host.UseSerilog((ctx, lc) => lc    
    .WriteTo.File(path: @"Logs\ApplicationLogs.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information
            ));


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
