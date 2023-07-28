using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using CNABSolution.Server.Models.Account;
using CNABSolution.Server.DatabaseConfig.Database;
using CNABSolution.Server.Controller.AccountController;
namespace CNABSolution.RoutesAPI;

public class AccountRoutesApi
{
    public static string local = "[RoutesAPI]";
    public static void MapRoutes(IEndpointRouteBuilder endpoint)
    {
        var accountCollection = Database.Client.GetDatabase("desafio_net").GetCollection<Account>("Account");
        endpoint.MapPost("/api/createAccount", async context =>
        {
            try
            {
                var accountController = new AccountController("ABC", "Wilman Anderson de Oliveira Martinez");
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
                var account_id = context.Request.RouteValues["id"].ToString();
                Account accountInfos = await AccountController.GetAccount(account_id);
                context.Response.StatusCode = 200;
                await context.Response.WriteAsJsonAsync(accountInfos);
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

    public static string DegineSucessMessage(string message, string jsonObject)
    {
        try
        {
            object jsonResult = new
            {
                Message = message,
                Result = jsonObject,
            };
            return JsonSerializer.Serialize(jsonResult);
        } catch (Exception error)
        {
            Console.Error.WriteLine($"{local} - Failed trying to define sucess message: {error}");
            throw new Exception(error.Message);
        }
    }
}