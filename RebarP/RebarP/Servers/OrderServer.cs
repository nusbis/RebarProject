using MongoDB.Driver;
using RebarP.Models;

namespace RebarP.Servers;

public class OrderServer
{
    private ConnectionDB connect;
    private IMongoCollection<Order> orderCollection;
    private readonly string collectionOrder = "order";
    public OrderServer()
    {
        connect = new ConnectionDB();
        orderCollection = connect.ConnectToMongoDB<Order>(collectionOrder);
    }

    public void Add(Order order)
    {
        orderCollection.InsertOne(order);
    }
}
