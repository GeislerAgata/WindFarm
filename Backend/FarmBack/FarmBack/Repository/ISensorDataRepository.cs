using FarmBack.DTO;

namespace FarmBack.Services;

public interface ISensorDataRepository
{
    List<SensorData> GetSensorsData();
    List<SensorData> GetSensorData(int sensorId);
    
    List<SensorData> GetSensorsDataBySensorType(string sensorType);
    List<SensorData> GetSensorsDataByTimestamp(DateTime timestamp);
    List<SensorData> GetSensorsDataByHour(string timeString);
    List<SensorData> GetSensorsDataByDate(DateTime timestamp);
    
    List<SensorData> GetSortedSensorData(string sortBy, bool ascending);
    
    List<SensorData> GetSensorDataByIdAndTime(int sensorId, DateTime timestamp);
    List<SensorData> GetSensorDataByTypeAndTime(string sensorType, DateTime timestamp);
}