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
#if RELEASE
string connection = Environment.GetEnvironmentVariable("connection");
#endif
#if DEBUG
var connection = builder.Configuration["MySqlConnection:ConnectionString"];
#endif

builder.Services.AddDbContext<MySQLContext>(options => options.UseSqlServer(connection));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarDealerRepository, CarDealerRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();


var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
