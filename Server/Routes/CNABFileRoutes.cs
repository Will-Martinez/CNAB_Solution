using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using BoletoNetCore;
using BoletoNetCore.Exceptions;
using BoletoNet;
using CNABSolution.Server.Models.Transfeers;
using CNABSolution.Server.Controller.CNABFileController;

namespace CNABSolution.RoutesCNAB;

public class CNABFileAPI
{
    public static string local = "[CNAB-ROUTES]";

    public static async Task MapRoutes(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/api/sendFile", async context =>
        {
            try
            {
                var file = context.Request.Form.Files["arquivo"];
                var transfeerController = new CNABFileController(file);
                List <Transfeers> allTransfeers = await transfeerController.TreatCnabFile();
                if (allTransfeers == null)
                {
                    context.Response.StatusCode = 400;
                    throw new Exception($"{local} - Failed trying to return all transfeers");
                }
                context.Response.StatusCode = 200;
                await context.Response.WriteAsJsonAsync(allTransfeers);
            } catch (Exception error)
            {
                Console.Error.Write($"{local} - Internal server error: {error}");
                context.Response.StatusCode = 500;
                throw new Exception(error.Message);
            }
        });
    }
}
