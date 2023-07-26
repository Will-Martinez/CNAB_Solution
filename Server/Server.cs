using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CNABSolution.Routes;
using CNABSolution.RoutesAPI;
using CNABSolution.Server.Database;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
if (Database.Client != null)
{
    app.UseEndpoints(endpoints =>
    {
        try
        {
            RoutesView.MapRoutes(endpoints);
            RoutesAPI.MapRoutes(endpoints);
        } catch (Exception error)
        {
            Console.Error.Write($"Failed trying to set routes: {error}");
            throw new Exception(error.Message);
        }
    });
    app.Run();
} else
{
    Console.Error.WriteLine("Server can not start if database it's connected.");
}
