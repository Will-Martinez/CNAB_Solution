using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CNABSolution.Routes
{
    public static class Routes
    {
        public static void MapRoutes(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/", context =>
            {
                try
                {
                    context.Response.Redirect("/home");
                    return Task.CompletedTask;
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Failed trying to redirect user: {error}");
                    throw new Exception(error.Message);
                }
            });

            endpoint.MapGet("/home", async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                try
                {
                    var pageContent = await File.ReadAllTextAsync("wwwroot/Public/Pages/Home.html");
                    await context.Response.WriteAsync(pageContent);
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Error: {error}");
                    throw new Exception(error.Message);
                }
            });
        }
    }
}
