using FarmBack.DTO;
using MongoDB.Driver;

namespace FarmBack.Services;
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly IMongoCollection<SensorData> _sensorDataCollection;

        public SensorDataRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _sensorDataCollection = database.GetCollection<SensorData>(collectionName);
        }
        
        public List<SensorData> GetSortedSensorData(string sortBy, bool ascending) // true - ascending, false - Descending
        {
            SortDefinition<SensorData> sortDefinition = ascending
                ? Builders<SensorData>.Sort.Ascending(sortBy)
                : Builders<SensorData>.Sort.Descending(sortBy);

            var sortedData = _sensorDataCollection.Find(Builders<SensorData>.Filter.Empty)
                .Sort(sortDefinition)
                .ToList();
            return sortedData;
        }

        public void InsertSensorData(SensorData sensorData)
        {
            _sensorDataCollection.InsertOne(sensorData);
        }
        public List<SensorData> GetSensorsData()
        {
            var filter = Builders<SensorData>.Filter.Exists(x => x.SensorId);
            return _sensorDataCollection.Find(filter).ToList();
        }
        
        public List<SensorData> GetSensorData(int sensorId)
        {
            var filter = Builders<SensorData>.Filter.Eq(x => x.SensorId, sensorId);
            return _sensorDataCollection.Find(filter).ToList();
        }

        public List<SensorData> GetSensorsDataByTime(DateTime timestamp)
        {
            var filter = Builders<SensorData>.Filter.Eq(x => x.Timestamp, timestamp);
            return _sensorDataCollection.Find(filter).ToList();
        }

        public List<SensorData> GetSensorsDataBySensorType(string sensorType)
        {
            var filter = Builders<SensorData>.Filter.Eq(x => x.SensorType, sensorType);
            return _sensorDataCollection.Find(filter).ToList();
        }

        public List<SensorData> GetSensorDataByIdAndTime(int sensorId, DateTime timestamp)
        {
            var filter = Builders<SensorData>.Filter.And(
                Builders<SensorData>.Filter.Eq(x => x.SensorId, sensorId),
                Builders<SensorData>.Filter.Eq(x => x.Timestamp, timestamp)
            );
            return _sensorDataCollection.Find(filter).ToList();
        }

        public List<SensorData> GetSensorDataByTypeAndTime(string sensorType, DateTime timestamp)
        {
            var filter = Builders<SensorData>.Filter.And(
                Builders<SensorData>.Filter.Eq(x => x.SensorType, sensorType),
                Builders<SensorData>.Filter.Eq(x => x.Timestamp, timestamp)
            );
            return _sensorDataCollection.Find(filter).ToList();
        }
    }
