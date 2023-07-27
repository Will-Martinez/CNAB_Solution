using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using CNABSolution.Server.Models.Account;
using CNABSolution.Server.Database;
using CNABSolution.Server.Controller.AccountController;
namespace CNABSolution.RoutesAPI;

public class RoutesAPI
{
    public static void MapRoutes(IEndpointRouteBuilder endpoint)
    {
        string local = "[RoutesAPI]";
        var accountCollection = Database.Client.GetDatabase("desafio_net").GetCollection<Account>("Account");
        endpoint.MapPost("/api/createAccount", async context =>
        {
            try
            {
                var accountController = new AccountController("DEF", "Gabriela Natacha de Oliveira Martinez");
                await accountController.CreateAccount();
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Account created!");
            } catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Internal server error: {error}");
                context.Response.StatusCode = 500;
                object errorMessage = DefineErrorMessage(error.Message);
                await context.Response.WriteAsJsonAsync(errorMessage);
            }
        });

        endpoint.MapGet("/api/getAccountById/{id}", async context =>
        {
            try
            {
                var teste = context.Request.RouteValues["id"];
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync($"ID value is {teste}");
            } catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Internal server error: {error}");
                context.Response.StatusCode = 500;
                object errorMessage = DefineErrorMessage(error.Message);
                await context.Response.WriteAsJsonAsync(errorMessage);
            }
        });
    }

    public static object DefineErrorMessage(string message)
    {
        object errorMessage = new
        {
            Error = message,
        };
        return errorMessage;
    }
}