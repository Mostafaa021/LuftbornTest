using Luftborn.Application.Common.Mapping;
using LuftbornTestApp;

var builder = WebApplication.CreateBuilder(args);

MapsterConfiguration.RegisterMappings(); // First configure all mappings

IWebHostEnvironment environment = builder.Environment;
var configuration = builder.Configuration
    .SetBasePath(environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.RegisterServices(configuration);

var app = builder.Build();

app.RegisterMiddleWares();

await app.RunAsync();