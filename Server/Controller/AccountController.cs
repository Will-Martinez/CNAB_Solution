using CNABSolution.Server.Models.Account;
using CNABSolution.Server.DatabaseConfig.Database;
using MongoDB.Driver;
namespace CNABSolution.Server.Controller.AccountController;

public class AccountController
{
    private string account_id;
    private string account_owner;
    private static IMongoCollection<Account> accountCollection = Database.Client.GetDatabase("desafio_net").GetCollection<Account>("Account");
    private static string local = "[Account-Controller]";
    public AccountController(string account_id, string account_owner)
    {
        this.account_id = account_id;
        this.account_owner = account_owner;
    }

    public async Task CreateAccount() {
        try
        {
            Account newAccount = new Account
            {
                account_id = account_id,
                account_owner = account_owner,
            };
            await accountCollection.InsertOneAsync(newAccount);
        } catch (Exception error)
        {
            throw new Exception(error.Message);
        }
    }

    public static async Task<Account> GetAccount(string account_id)
    {
        try
        {
            var filter = Builders<Account>.Filter.Eq(x => x.account_id, account_id);
            Account data = await accountCollection.Find(filter).FirstOrDefaultAsync();
            return data;
        } catch (Exception error)
        {
            Console.Error.WriteLine($"{local} - Failed trying to find account: {error}");
            throw new Exception(error.Message);
        }
    }
}
