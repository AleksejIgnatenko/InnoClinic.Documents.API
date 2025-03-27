using InnoClinic.Documents.API.Extensions;
using InnoClinic.Documents.API.Middlewares;
using InnoClinic.Documents.Application.Core;
using InnoClinic.Documents.Application.Services;
using InnoClinic.Documents.DataAccess.Core;
using InnoClinic.Documents.DataAccess.Repositories;
using InnoClinic.Documents.Infrastructure.Minio;
using Microsoft.Extensions.Options;
using Minio;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .CreateSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MinioSettings>(builder.Configuration.GetSection("Minio"));
builder.Services.AddSingleton(provider =>
{
    var minioSettings = provider.GetRequiredService<IOptions<MinioSettings>>().Value;
    return new MinioClient()
        .WithEndpoint(minioSettings.Endpoint)
        .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey)
        .Build();
});

builder.Services.AddTransient<IValidationService, ValidationService>();

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:4000", "http://localhost:4001");
    x.WithMethods().AllowAnyMethod();
});

app.Run();