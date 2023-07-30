using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using CNABSolution.Server.Models.Transfeers;
using CNABSolution.Server.Controller.CNABFileController;

namespace CNABSolution.RoutesCNAB;

public class CNABFileAPI
{
    public static string local = "[CNAB-ROUTES]";

    public static void MapRoutes(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/api/sendFile", async context =>
        {
            try
            {
                var file = context.Request.Form.Files["arquivo"];
                if(file == null)
                {
                    context.Response.StatusCode = 400;
                    Console.WriteLine($"{local} - Failed trying to send cnab file.");
                    throw new Exception("CNAB file is required to transfeers saving.");
                }
                var transfeerController = new CNABFileController(file);
                List<Transfeers>allTransfeers = await transfeerController.TreatCnabFile();
                if (allTransfeers == null)
                {
                    context.Response.StatusCode = 404;
                    throw new Exception($"{local} - Failed trying to return all transfeers");
                }
                context.Response.StatusCode = 200;
                await context.Response.WriteAsJsonAsync(allTransfeers);
            } catch (Exception error)
            {
                context.Response.StatusCode = 500;
                Console.Error.Write($"{local} - Internal server error: {error}");
                throw;
            }
        });
    }
}
