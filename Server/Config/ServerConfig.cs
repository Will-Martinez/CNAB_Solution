using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CNABSolution.RoutesView;
using CNABSolution.Server.DatabaseConfig.Database;
using CNABSolution.RoutesTransaction;
using Microsoft.AspNetCore.Builder;
using System;
namespace CNABSolution.Server.Config.ServerConfig
{
    // Classe responsável por configurar rotas, iniciar o builder webapp e chamar a conexão com banco de dados
    public class ServerConfig
    {
        public static string local = "[SERVER-CONFIG]";

        // Método estático e public acessivel para o arquivo server.cs consiga iniciar o app sem
        // necessidade de criar uma instancia do tipo ServerConfig
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

        // Método privado acessivel somente a classe ServerConfig para de fato realizar as configurações do servidor
        private static void ConfigureMiddlewares(IApplicationBuilder app)
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
                            RoutesAPIView.MapRoutes(endpoints);
                            TransactionsAPI.MapRoutes(endpoints);
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