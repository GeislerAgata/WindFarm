using FarmBack.DTO;

namespace FarmBack.Services;

public interface ISensorDataRepository
{
    List<SensorData> GetSensorData(int sensorId);
}