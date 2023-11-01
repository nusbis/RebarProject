using MongoDB.Driver;
using RebarP.Models;

namespace RebarP.Servers;

public class ShakeServe
{
    private ConnectionDB connect;
    private IMongoCollection<Shake> shakeCollection;
    private readonly string collectionShake = "shake";
    public ShakeServe()
    {
        connect = new ConnectionDB();
        shakeCollection = connect.ConnectToMongoDB<Shake>(collectionShake);
    }

    public List<Shake> GetAll()
    {
        return shakeCollection.Find(shake => true).ToList();
    }
    public Shake GetById(Guid id)
    {
        return shakeCollection.Find(shake => shake.ID == id).FirstOrDefault();
    }
    public void Add(Shake shake)
    {
        shakeCollection.InsertOne(shake);
    }
    public void Update(Shake shake)
    {
        var filter = Builders<Shake>.Filter.Eq("Id", shake.ID);
        shakeCollection.ReplaceOne(filter, shake);
    }
    public void Delete(Guid id)
    {
        shakeCollection.DeleteOne(s => s.ID == id);
    }

}
