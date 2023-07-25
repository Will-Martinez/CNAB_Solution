using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CNABSolution.Routes;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
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
