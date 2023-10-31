﻿using MongoDB.Driver;
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
    public void Add(Shake shake)
    {
        shakeCollection.InsertOne(shake);
    }
    public void Update(Shake shake)
    {
        var filter = Builders<Shake>.Filter.Eq("Id", shake.ID);
        shakeCollection.ReplaceOne(filter, shake);
        //  return shakeCollection.ReplaceOneAsync(filter, shake, new ReplaceOptions { IsUpsert = true });
    }
    public void Delete(Guid id)
    {
        shakeCollection.DeleteOne(s => s.ID == id);
        // return shakeCollection.DeleteOneAsync(s => s.Id == shake.Id);
    }

}
