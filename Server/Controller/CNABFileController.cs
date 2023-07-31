
using CNABSolution.Server.DatabaseConfig.Database;
using CNABSolution.Server.Models.Transfeers;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace CNABSolution.Server.Controller.CNABFileController
{
    public class CNABFileController
    {
        public static string local = "[CNABFILE-CONTROLLER]";
        public static IMongoCollection<Transfeers> transfeersCollection = Database.Client.GetDatabase("desafio_net").GetCollection<Transfeers>("Transfeers");
        private IFormFile cnabFile;
        public CNABFileController(IFormFile file)
        {
            this.cnabFile = file;
        }

        public async Task<List<Transfeers>> TreatCnabFile()
        {
            try
            {
                string fileContent = await ReadFileContent(cnabFile);
                string[] lines = fileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                List<Transfeers> allTransactions = new List<Transfeers>();

                foreach (var line in lines)
                {
                    List<Transfeers> transactions = ParseCnabFile(line);
                    allTransactions.AddRange(transactions);
                }
                await RegisterTransaction(allTransactions);
                return allTransactions;
            } catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Failed trying to Configure file content: {error}");
                throw new Exception(error.Message);
            }
        }

        private async Task<string> ReadFileContent(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private List<Transfeers> ParseCnabFile(string line)
        {
            try
            {
                List<Transfeers> transactions = new List<Transfeers>();
                string tipoTransacao = line.Substring(0, 1);
                string dataOcorrencia = line.Substring(1, 8);
                string valor = line.Substring(9, 10);
                string cpf = line.Substring(19, 11);
                string cartao = line.Substring(30, 12);
                string donoLoja = line.Substring(42, 14);
                string nomeLoja = line.Substring(56, 18);

                Transfeers data = new Transfeers
                {
                    type = tipoTransacao,
                    transaction_date = dataOcorrencia,
                    amount = valor,
                    cpf = cpf,
                    card_number = cartao,
                    store_owner = donoLoja,
                    store_name = nomeLoja,
                };

                transactions.Add(data);

                return transactions;

            }
            catch (Exception error)
            {
                Console.Error.WriteLine($"{local} - Failed trying to parse cnab file: {error}");
                throw new Exception(error.Message);
            }
        }
        private async Task RegisterTransaction(List<Transfeers> transactions)
        {
            try
            {
                await transfeersCollection.InsertManyAsync(transactions);
            } catch (Exception error)
            {
                Console.Error.WriteLine(error);
                throw new Exception(error.Message);
            }
        }

    }
}
