using CNABSolution.Server.Config.ServerConfig;
namespace CNABSolution.Server;

// Classe principal. core da aplica��o para iniciar o servidor.
public class Server
{
    public static string local = "[SERVER-MAIN]";

    //M�todo Main para inicar a aplica��o
    private static void Main(string[] args)
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
