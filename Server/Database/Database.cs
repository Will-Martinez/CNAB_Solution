using MongoDB.Driver;
using System.Net.NetworkInformation;

namespace CNABSolution.Server.Database;

public class Database
{
    public static bool StartConnection()
    {
        string local = "[DB-CONNECTION]";
        try
        {
            string databaseUrl = "mongodb://localhost:27017/desafio_net";
            var client = new MongoClient(databaseUrl);
            Console.WriteLine($"{local} - Connected to database");
            return true;
        } catch (Exception error)
        {
            Console.Error.WriteLine($"{local} - Failed trying to start connection: {error}");
            return false;
        }
    }
}
