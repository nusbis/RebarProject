using MongoDB.Driver;
namespace RebarP;

public class ConnectionDB
{

    private const string connectionString = "mongodb://127.0.0.1:27017";
    private const string databaseName = "reber_db";
    public IMongoCollection<T> ConnectToMongoDB<T>(string collection)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        return db.GetCollection<T>(collection);
    }
}
