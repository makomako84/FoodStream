using Microsoft.Extensions.Configuration;
using Foodstream.Infrastructure;
using Foodstream.Application.Interfaces;
using Foodstream.Application.Services;
using Foodstream.Application.Configuration;
using Foodstream.Infrastructure.Postgre;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;

var builder = WebApplication.CreateBuilder(args);

// postgre context
builder.Services
    .AddPostgreCoreContext(builder.Configuration["PostgreConnection:ConnectionString"]);

// s3
var s3Credentials = builder.Configuration
    .GetSection(S3CredentialsOptions.Section)
    .Get<S3CredentialsOptions>();
var s3Options = builder.Configuration.GetAWSOptions();
s3Options.Credentials = new Amazon.Runtime.BasicAWSCredentials(
    s3Credentials.S3AccessKeyId, 
    s3Credentials.S3SecretAccessKey);
builder.Services.AddDefaultAWSOptions(s3Options);
builder.Services.AddAWSService<IAmazonS3>();

builder.Services
    .Configure<S3Options>(builder.Configuration.GetSection(S3Options.Section));

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
