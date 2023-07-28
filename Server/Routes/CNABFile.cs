using System.Text.Json;
using System.Text.Json.Serialization;
namespace CNABSolution.RoutesCNAB;

public class CNABFileAPI
{
    public static string local = "[CNAB-ROUTES]";

    public static void MapRoutes(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/api/CNABFile", async context =>
        {
            try
            {
                var file = context.Request.Form.Files["arquivo"];
                Console.WriteLine($"File result: {JsonSerializer.Serialize(file)}");
            } catch (Exception error)
            {
                Console.Error.Write($"{local} - Internal server error: {error}");
                context.Response.StatusCode = 500;
                throw new Exception(error.Message);
            }
        });
    }
}
