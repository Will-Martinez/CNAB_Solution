using CNABSolution.Server.Config.ServerConfig;
namespace CNABSolution.Server;

public class Server
{
    public static string local = "[SERVER-MAIN]";
    public static void Main(string[] args)
    {
        try
        {
            ServerConfig.BuildWebApp(args);
        } catch (Exception error)
        {
            Console.Error.WriteLine($"{local} - Failed trying to start server: {error}");
            throw new Exception(error.Message);
        }
    }
}
