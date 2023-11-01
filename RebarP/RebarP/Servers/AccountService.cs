using MongoDB.Driver;
using RebarP.Models;

namespace RebarP.Servers;

public class AccountService
{
    private ConnectionDB connect;
    private IMongoCollection<Account> accountCollection;
    private readonly string stringCollectionAcount = "account";
    public AccountService()
    {
        connect = new ConnectionDB();
        accountCollection = connect.ConnectToMongoDB<Account>(stringCollectionAcount);
    }

    public Account GetById(Guid id)
    {
        return accountCollection.Find(account => account.ID == id).FirstOrDefault();
    }
    public void Add(Account account)
    {
        accountCollection.InsertOne(account);
    }

}
