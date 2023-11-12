using FarmBack.DTO;
using MongoDB.Driver;

namespace FarmBack.Services;

public class SensorDataRepository
{
    private readonly IMongoCollection<SensorData> _sensorDataCollection;

    public SensorDataRepository(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _sensorDataCollection = database.GetCollection<SensorData>(collectionName);
    }

    public void InsertSensorData(SensorData sensorData)
    {
        _sensorDataCollection.InsertOne(sensorData);
    }

    public List<SensorData> GetSensorData(int sensorId)
    {
        var filter = Builders<SensorData>.Filter.Eq(x => x.SensorId, sensorId);
        return _sensorDataCollection.Find(filter).ToList();
    }
}