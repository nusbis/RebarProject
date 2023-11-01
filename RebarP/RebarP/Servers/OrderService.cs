using MongoDB.Driver;
using RebarP.Models;

namespace RebarP.Servers;

public class OrderService
{
    private ConnectionDB connect;
    private IMongoCollection<Order> orderCollection;
    private readonly string collectionOrder = "order";
    public OrderService()
    {
        connect = new ConnectionDB();
        orderCollection = connect.ConnectToMongoDB<Order>(collectionOrder);
    }

    public Order Add(Order order)
    {
        orderCollection.InsertOne(order);
        return order;
    }

    public void Delete(Guid id)
    {
        orderCollection.DeleteOne(s => s.ID == id);
    }

    public Order GetById(Guid id)
    {
        Order order= orderCollection.Find(order => order.ID == id).FirstOrDefault();
        if (order == null) throw new Exception("this id Of Order isnt exsist");
        return order;
    }


    public List<Order> GetAllOrdersById(List<Guid> idsOfToday)
    {
        return idsOfToday.Select(id => GetById(id)).Where(order=>order.EndOrder==DateTime.Today).ToList();

    }
}
