using AutoMapper;
using CarShopping.Config;
using CarShopping.Model.Context;
using CarShopping.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration["MySqlConnection:ConnectionString"];

builder.Services.AddDbContext<MySQLContext>(opt => opt.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 29))));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarDealerRepository, CarDealerRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();


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
