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
    public Account Update(Account acc)
    {
        var filter = Builders<Account>.Filter.Eq("ID", acc.ID);
        accountCollection.ReplaceOne(filter, acc);
        return acc;
    }
    public Account AddOrderToAccount(Guid id, Account acc)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("id");
        if (IsIdOrderExsistInThisAccount(id, acc))
            throw new Exception("this order exsist in our account");
        acc.ListOfOrderIDs.Add(id);
        acc = Update(acc);
        return acc;
    }
    private bool IsIdOrderExsistInThisAccount(Guid id, Account acc)
    {
        if (acc.ListOfOrderIDs == null || acc.ListOfOrderIDs.Count == 0)
            return false;
        return acc.ListOfOrderIDs.FirstOrDefault(thisId => thisId == id)!=null;
       
    }

}
