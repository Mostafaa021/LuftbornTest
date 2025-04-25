using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Luftborn.Infrastructure.Presistance.Data.Customs;
using Microsoft.AspNetCore.Mvc;

namespace LuftbornTestApp.CustomMiddleWares;

public class GlobalExceptionHandler 
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var problem = new ProblemDetails
            {
                Title = context.Response.StatusCode == 500 ? "An unexpected error occurred" : "An error occurred",
                Status = context.Response.StatusCode,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            // If custom exception, you can enrich it
            if (ex is CustomException customEx)
            {
                problem.Extensions["errorCode"] = customEx.ErrorCode;
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem, options));
        }
    }
}