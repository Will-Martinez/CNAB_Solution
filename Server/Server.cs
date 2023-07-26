using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CNABSolution.Routes;
using CNABSolution.Server.Database;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
if (Database.StartConnection())
{
    app.UseEndpoints(endpoints =>
    {
        try
        {
            RoutesView.MapRoutes(endpoints);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Error: {error}");
            throw new Exception(error.Message);
        }
    });

    app.Run();
} else
{
    Console.Error.WriteLine("Server can not be started if database it's connedted.");
}
