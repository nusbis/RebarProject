using MongoDB.Driver;
using RebarP.Models;

namespace RebarP.Servers;

public class CheckoutService
{
    private ConnectionDB connect;
    private IMongoCollection<Checkout> accountCollection;
    private readonly string stringCollectionCheckout = "checkout";
    public CheckoutService()
    {
        connect = new ConnectionDB();
        accountCollection = connect.ConnectToMongoDB<Checkout>(stringCollectionCheckout);
    }

    public Checkout GetById(Guid id)
    {
        return accountCollection.Find(checkout => checkout.ID == id).FirstOrDefault();
    }

    public Checkout GetByPassword(string password)
    {
        return accountCollection.Find(checkout => checkout.Password == password).FirstOrDefault();
    }
    public void Add(Checkout checkout)
    {
        accountCollection.InsertOne(checkout);
    }
    public Checkout Update(Checkout checkout)
    {
        var filter = Builders<Checkout>.Filter.Eq("ID", checkout.ID);
        accountCollection.ReplaceOne(filter, checkout);
        return checkout;
    }
    public Checkout AddOrderToAccount(Guid id, Checkout checkout)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("id is empty");
        if (IsIdOrderExsistInThisAccount(id, checkout))
            throw new Exception("order exsist in our account");
        checkout.ListOfOrderIDs.Add(id.ToString());
        checkout = Update(checkout);
        return checkout;
    }
    private bool IsIdOrderExsistInThisAccount(Guid id, Checkout checkout)
    {
        if (checkout.ListOfOrderIDs == null || checkout.ListOfOrderIDs.Count == 0)
            return false;
        return checkout.ListOfOrderIDs.FirstOrDefault(thisId => thisId.Equals( id))!=null;
       
    }

}
