using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CNABSolution.Routes;
using CNABSolution.RoutesAPI;
using CNABSolution.Server.DatabaseConfig.Database;
using CNABSolution.RoutesCNAB;
using Microsoft.AspNetCore.Builder;
using System;
namespace CNABSolution.Server.Config.ServerConfig
{
    public class ServerConfig
    {
        public static string local = "[SERVER-CONFIG]";

        public static void BuildWebApp(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var app = builder.Build();
                ConfigureMiddlewares(app);
                app.Run();
            }
            catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Failed trying to build web app: {error}");
                throw new Exception(error.Message);
            }
        }

        public static void ConfigureMiddlewares(IApplicationBuilder app)
        {
            try
            {
                app.UseRouting();
                app.UseStaticFiles();
                if (Database.Client != null)
                {
                    app.UseEndpoints(endpoints =>
                    {
                        try
                        {
                            RoutesView.MapRoutes(endpoints);
                            AccountRoutesApi.MapRoutes(endpoints);
                            CNABFileAPI.MapRoutes(endpoints);
                        }
                        catch (Exception error)
                        {
                            Console.Error.Write($"Failed trying to set routes: {error}");
                            throw new Exception(error.Message);
                        }
                    });
                }
                else
                {
                    Console.Error.WriteLine("Server can not start if database it's not connected.");
                }
            }
            catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Failed trying to configure server middlewares: {error}");
                throw new Exception(error.Message);
            }
        }
    }
}