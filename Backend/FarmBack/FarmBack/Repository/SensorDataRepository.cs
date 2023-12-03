using FarmBack.DTO;
using MongoDB.Driver;
using System.Collections.Generic;

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
            SortDefinition<SensorData> sortDefinition = null;

            switch (sortBy)
            {
                case "timestamp":
                    sortDefinition = ascending
                        ? Builders<SensorData>.Sort.Ascending(data => data.Timestamp)
                        : Builders<SensorData>.Sort.Descending(data => data.Timestamp);
                    break;
                case "sensor_id":
                    sortDefinition = ascending
                        ? Builders<SensorData>.Sort.Ascending(data => data.SensorId)
                        : Builders<SensorData>.Sort.Descending(data => data.SensorId);
                    break;
                case "sensor_type":
                    sortDefinition = ascending
                        ? Builders<SensorData>.Sort.Ascending(data => data.SensorType)
                        : Builders<SensorData>.Sort.Descending(data => data.SensorType);
                    break;
                case "value":
                    sortDefinition = ascending
                        ? Builders<SensorData>.Sort.Ascending(data => data.Value)
                        : Builders<SensorData>.Sort.Descending(data => data.Value);
                    break;
                case "unit":
                    sortDefinition = ascending
                        ? Builders<SensorData>.Sort.Ascending(data => data.Unit)
                        : Builders<SensorData>.Sort.Descending(data => data.Unit);
                    break;
                default:
                    sortDefinition = ascending
                        ? Builders<SensorData>.Sort.Ascending(data => data.Value)
                        : Builders<SensorData>.Sort.Descending(data => data.Value);
                    break;
            }

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

        public List<SensorData> GetSensorsData(string filters, string sortBy, string order)
        {
            
            var filterBy = System.Text.Json.JsonSerializer.Deserialize<Filters>(filters);

            //var data = new List<SensorData>();

            var filter = Builders<SensorData>.Filter.Empty;
            SortDefinition<SensorData> sort = null;

            if (!string.IsNullOrEmpty(filterBy?.SensorId))
            {
                var sensorIdFilter = Builders<SensorData>.Filter.Eq(x => x.SensorId, int.Parse(filterBy.SensorId));
                filter = Builders<SensorData>.Filter.And(filter, sensorIdFilter);
            }
            if (!string.IsNullOrEmpty(filterBy?.SensorType))
            {
                var sensorTypeFilter = Builders<SensorData>.Filter.Eq(x => x.SensorType, filterBy.SensorType);
                filter = Builders<SensorData>.Filter.And(filter, sensorTypeFilter);
            }
            if (!string.IsNullOrEmpty(filterBy?.Timestamp))
            {
                DateTime startOfDay = DateTime.Parse(filterBy.Timestamp);
                DateTime endOfDay = startOfDay.AddDays(1).AddTicks(-1);

                var timestampFilter = Builders<SensorData>.Filter.Gte(x => x.Timestamp, startOfDay) &
                             Builders<SensorData>.Filter.Lt(x => x.Timestamp, endOfDay);
                filter = Builders<SensorData>.Filter.And(filter, timestampFilter);
            }

            if (sortBy != "") {
                if (order == "asc")
                {
                    sort = Builders<SensorData>.Sort.Ascending(sortBy);
                }
                else if (order == "desc")
                {
                    sort = Builders<SensorData>.Sort.Descending(sortBy);
                }

            }

            if (sort != null)
            {
                var sortedData = _sensorDataCollection.Find(filter)
                    .Sort(sort)
                    .ToList();

                return sortedData;
            }
            return _sensorDataCollection.Find(filter).ToList();
        }

        public List<SensorData> GetSensorData(int sensorId)
        {
            var filter = Builders<SensorData>.Filter.Eq(x => x.SensorId, sensorId);
            return _sensorDataCollection.Find(filter).ToList();
        }

        public List<SensorData> GetSensorsDataByTimestamp(DateTime timestamp)
        {
            var filter = Builders<SensorData>.Filter.Eq(x => x.Timestamp, timestamp);
            return _sensorDataCollection.Find(filter).ToList();
        }
        
        public List<SensorData> GetSensorsDataByHour(string timeString)
        {
            if (TimeSpan.TryParse(timeString, out TimeSpan time))
            {
                DateTime today = DateTime.Today;
                DateTime searchTime = today.Date.Add(time);

                var filter = Builders<SensorData>.Filter.Where(x => x.Timestamp.Hour == searchTime.Hour &&
                                                                    x.Timestamp.Minute == searchTime.Minute &&
                                                                    x.Timestamp.Second == searchTime.Second);

                return _sensorDataCollection.Find(filter).ToList();
            }
            else
            {
                return new List<SensorData>();
            }
        }
        
        public List<SensorData> GetSensorsDataByDate(DateTime timestamp)
        {
            DateTime startOfDay = timestamp.Date;
            DateTime endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            var filter = Builders<SensorData>.Filter.Gte(x => x.Timestamp, startOfDay) &
                         Builders<SensorData>.Filter.Lt(x => x.Timestamp, endOfDay);

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
