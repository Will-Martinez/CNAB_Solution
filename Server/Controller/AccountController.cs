using CNABSolution.Server.Models.Account;
using MongoDB.Driver;
namespace CNABSolution.Server.Controller.AccountController;

public class AccountController
{
    private string account_id;
    private string account_owner;
    private IMongoCollection<Account> accountCollection = Database.Database.Client.GetDatabase("desafio_net").GetCollection<Account>("Account");
    private string local = "[Account-Controller]";
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
            Console.Error.WriteLine($"{local} - Failed trying to create a new account: {error}");
            throw new Exception(error.Message);
        }
    }
}
