using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TurboProject.DataLayer;
using TurboProject.BusinessLayer;
using TurboProject.APILayer.Middleware;
using TurboProject.APILayer;
using TurboProject.DomainLayer;
using TurboProject.BusinessLayer.Model.Smtp;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.BusinessLayer.Service.Impl;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Turboaz API",
        Version = "v1",
        Description = "API documentation for Turboaz project"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter 'Bearer {your JWT token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});




services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});
services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
services.AddScoped<IEmailService, EmailService>();
services.AddRedisCache(configuration);
services.AddDataAccessLayerConfig(configuration);
services.AddDomainLayerConfig();
services.AddBusinessLayerConfig(configuration);
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Logging.AddHybridLogging(configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.SeedRolesAsync();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); 


app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
