using Microsoft.Extensions.Configuration;
using Foodstream.Infrastructure;
using Foodstream.Application.Interfaces;
using Foodstream.Application.Services;
using Foodstream.Infrastructure.Postgre;

var builder = WebApplication.CreateBuilder(args);

// persistence
builder.Services
    .AddPostgreCoreContext(builder.Configuration["PostgreConnection:ConnectionString"]);

// services
builder.Services.AddScoped<IPointRepository, PointRepository>();
builder.Services.AddScoped<IPointService, PointService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
