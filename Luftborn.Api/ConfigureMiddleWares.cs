using Asp.Versioning.ApiExplorer;
using LuftbornTestApp.CustomMiddleWares;
using LuftbornTestApp.Extensions;



namespace LuftbornTestApp;

public  static class ConfigureMiddleWares
{
    public static void RegisterMiddleWares(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Testing")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts(); // for TLS security  
        }
         // for using Serilog as Logging (Register SerilogMiddleWare) ,but I will use here built in logging
        
        app.UseExceptionHandler(builder => builder.ExceptionHandlerConfig()); // extend  built in exception handler for standardization 
        //app.UseMiddleware<GlobalExceptionHandler>(); // for using custom exception handler
        
        app.UseHttpsRedirection();
        app.UseFileServer();  // include three middlewares: UseStaticFiles, UseDefaultFiles, UseDirectoryBrowser
        app.UseRouting(); 
         // Also I can inject RateLimiting , Caching  and Forward middlewares here 

        //CORS must be after UseRouting and before UseAuthorization & UseEndpoints
        if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Testing")
        {
            // Simplified CORS for development environment
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
        else
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type", 
                    "ETag", "Location", "X-Pagination", "X-Response-Signature")
                .WithOrigins("https://luftborn.com")); // Production Domain 
        }

        //   app.UseAuthentication();
        //   app.UseAuthorization();
        
        // If you are using MapControllers, you don't need to use UseEndpoints

        app.MapControllers();

        app.Run();
    }
}

