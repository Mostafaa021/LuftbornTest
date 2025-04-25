using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Luftborn.Infrastructure.Presistance.Data.Customs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace LuftbornTestApp.Extensions;

public static class ConfigurationExtensions
    {
        public static void ExceptionHandlerConfig(this IApplicationBuilder builder)
        {
            builder.Run(async context =>
            {
                // Get error details
                var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandler != null)
                {
                    context.Response.StatusCode = exceptionHandler.Error switch
                    {
                        NotFoundException => (int)HttpStatusCode.NotFound,
                        ValidationException => (int)HttpStatusCode.BadRequest,
                        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    if (exceptionHandler.Error is CustomException)
                    {
                        await context.Response.WriteAsJsonAsync(new ProblemDetails
                        {
                            Title = "An error occurred",
                            Status = context.Response.StatusCode,
                            Detail = exceptionHandler.Error.Message
                        });
                    }
                    else
                    {
                        if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                        {
                            await context.Response.WriteAsJsonAsync(new ProblemDetails
                            {
                                Title = "An unexpected error occurred",
                                Status = context.Response.StatusCode,
                                Detail = exceptionHandler.Error.Message
                            });
                        }
                        else
                        {
                            // Return message for other errors
                            await context.Response.WriteAsJsonAsync(
                                new ProblemDetails 
                                    { Detail = exceptionHandler.Error.Message }
                                );
                        }
                    }
                }
            });
        }
        // This method configures Swagger/OpenAPI documentation for API. It performs two main functions:
        // 1. XML Documentation Integration
        // 2. Security Definition for JWT Authentication
        public static void SwaggerGenConfig(this SwaggerGenOptions options)
        {
          
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            //Enable Authentication
            options.AddSecurityDefinition("JWT",
                new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        }
                    },
                    Array.Empty<string>()
                    }
                });
        }

        // This method configures the Swagger UI for your API. It sets up the Swagger UI to display the API documentation.
        public static void SwaggerUIConfig(this SwaggerUIOptions options, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        }
    }