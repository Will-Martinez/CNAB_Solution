using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CNABSolution.RoutesView
{
    // Classe respons�vel por renderizar as views no lado do cliente
    public static class RoutesAPIView
    {
        public static string local = "[ROUTES-VIEW]";

        // M�todo publico e est�tico para que o arquivo de configura��o consiga acessar esse m�todo
        public static void MapRoutes(IEndpointRouteBuilder endpoint)
        {
            // endpoint para que caso o usu�rio entre no core da aplica��o/rota printipal, ele � redirecionado para home
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

            // endpoint respons�vel por renderizar o view de home
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

            // endpoint respons�vel por renderizar o view de Upload(upload do arquivo cnab)
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

            // endpoint respons�vel por renderizar o view de transactions com os dados de transa��o
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
