namespace CNABSolution.Server.Helper
{
    public class Helper
    {
        public static string local = "[HELPER]";

        public static object DefineMessage(string message, int responseCode, string type)
        {
            try
            {
                object errorMessage = new
                {
                    Status = responseCode,
                    type = type,
                    message = message,
                };

                return errorMessage;
            } catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Failed trying to define a error message: {error}");
                throw new Exception(error.Message);
            }
        }
    }
}
