using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CNABSolution.Routes
{
    public static class RoutesView
    {
        public static string local = "[ROUTES-VIEW]";
        public static void MapRoutes(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/", context =>
            {
                try
                {
                    context.Response.StatusCode = 200;
                    context.Response.Redirect("/home");
                    return Task.CompletedTask;
                }
                catch (Exception error)
                {
                    context.Response.StatusCode = 500;
                    Console.Error.WriteLine($"{local} - Failed trying to redirect user to home page: {error}");
                    throw;
                }
            });

            endpoint.MapGet("/home", async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                try
                {
                    string pagePath = "wwwroot/Public/Pages/Home.html";
                    var pageContent = await File.ReadAllTextAsync(pagePath);
                    if(!File.Exists(pagePath))
                    {
                        context.Response.StatusCode = 404;
                        Console.Error.WriteLine($"{local} - Failed trying to render Home html page");
                        throw new Exception("Home page not founded.");
                    }
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(pageContent);
                }
                catch (Exception error)
                {
                    context.Response.StatusCode = 500;
                    Console.Error.WriteLine($"{local} - Failed trying to access Home page: {error}");
                    throw;
                }
            });
            endpoint.MapGet("/upload", async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                try
                {
                    string pagePath = "wwwroot/public/Pages/Upload.html";
                    var pageContent = await File.ReadAllTextAsync(pagePath);
                    if(!File.Exists(pagePath))
                    {
                        context.Response.StatusCode = 404;
                        Console.Error.WriteLine($"{local} - Failed trying to render Solution html page");
                        throw new Exception("Solution page not founded.");
                    }
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(pageContent);
                } catch (Exception error)
                {
                    Console.Error.WriteLine($"{local} - Failed trying to access solution page: {error}");
                    throw;
                }
            });

            endpoint.MapGet("/transactions", async context =>
            {
                try
                {
                    string pagePath = "wwwroot/public/Pages/Transaction.html";
                    var pageContent = await File.ReadAllTextAsync(pagePath);
                    if(!File.Exists(pagePath))
                    {
                        context.Response.StatusCode = 404;
                        Console.Error.WriteLine($"{local} - Failed trying to render tranfeers html page");
                        throw new Exception("Transaction page not founded");
                    }
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(pageContent);
                } catch (Exception error)
                {
                    Console.Error.WriteLine($"{local} - Failed trying to access tranfeers page: {error}");
                    throw;
                }
            });
        }
    }
}
