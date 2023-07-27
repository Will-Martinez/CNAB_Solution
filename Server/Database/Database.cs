using MongoDB.Driver;
using System.Net.NetworkInformation;

namespace CNABSolution.Server.DatabaseConfig.Database;

public class Database
{
    public static MongoClient Client { get; set; } = StartConnection();

    public static MongoClient StartConnection()
    {
        string local = "[DB-CONNECTION]";
        try
        {
            string databaseUrl = "mongodb://localhost:27017/desafio_net";
            Client = new MongoClient(databaseUrl);
            Console.WriteLine($"{local} - Connected.");
            return Client;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"{local} - Failed trying to start connection: {error}");
            throw new Exception(error.Message);
        }
    }
}
