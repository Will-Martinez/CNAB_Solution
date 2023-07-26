using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using CNABSolution.Server.Models.Account;
using CNABSolution.Server.Database;

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
                var newAccount = new Account
                {
                    account_id = "ABC",
                    account_owner = "Wilman Anderson de Oliveira Martinez",
                };
                await accountCollection.InsertOneAsync(newAccount);
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Account created!");
            } catch (Exception error)
            {
                context.Response.StatusCode = 500;
                Console.Error.WriteLine($"{local} - Failed trying to create a new Account: {error}");
                throw new Exception(error.Message);
            }
        });
    }
}