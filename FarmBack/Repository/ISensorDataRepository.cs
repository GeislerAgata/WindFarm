using FarmBack.DTO;

namespace FarmBack.Services;

public interface ISensorDataRepository
{
    List<SensorData> GetSensorsData();
    List<SensorData> GetSensorsDataBySensorType(string sensorType);
    List<SensorData> GetSensorsDataByTime(DateTime timestamp);
    List<SensorData> GetSensorData(int sensorId);
    List<SensorData> GetSortedSensorData(string sortBy, bool ascending);
    List<SensorData> GetSensorDataByIdAndTime(int sensorId, DateTime timestamp);
    List<SensorData> GetSensorDataByTypeAndTime(string sensorType, DateTime timestamp);
}