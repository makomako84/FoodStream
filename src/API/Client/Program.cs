using Microsoft.Extensions.Configuration;
using Foodstream.Infrastructure;
using Foodstream.Application.Interfaces;
using Foodstream.Application.Services;
using Foodstream.Infrastructure.Postgre;
using Foodstream.Application;
using Amazon.S3;
using Foodstream.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPostgre(builder.Configuration)
    .AddInfrastracture(builder.Configuration);

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
