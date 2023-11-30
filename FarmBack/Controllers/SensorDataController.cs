using FarmBack.DTO;
using FarmBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorDataController : ControllerBase
{
    private readonly ISensorDataRepository _repository;
    //private readonly SensorDataRepository _repository;

    public SensorDataController(ISensorDataRepository repository)
    //public SensorDataController()
    {
        //_repository = new SensorDataRepository("mongodb://localhost:27017", "windfarm", "windfarm");
        this._repository = repository;
    }

    // GET /SensorData
    [HttpGet]
    public List<SensorData> GetSensorsData()
    {
        return _repository.GetSensorsData();
    }

    // GET /SensorData/id/{sensorId}
    [HttpGet("{sensorId}")]
    public ActionResult<List<SensorData>> GetSensorData(int sensorId)
    {
        var data = _repository.GetSensorData(sensorId);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }
    
    // GET /SensorData/time/{timestamp}
    [HttpGet("time/{timestamp}")]
    public ActionResult<List<SensorData>> GetSensorsDataByTime(DateTime timestamp)
    {
        var data = _repository.GetSensorsDataByTime(timestamp);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }

    // GET /SensorData/type/{sensorType}
    [HttpGet("type/{sensorType}")]
    public ActionResult<List<SensorData>> GetSensorsDataBySensorType(string sensorType)
    {
        var data = _repository.GetSensorsDataBySensorType(sensorType);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }

    // GET /SensorData/sortBy/{sortBy}/trend/{ascending}
    [HttpGet("sortBy/{sortBy}/trend/{ascending}")]
    public ActionResult<List<SensorData>> GetSortedSensorData(string sortBy, bool ascending)
    {
        var data = _repository.GetSortedSensorData(sortBy, ascending);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }

    // GET /SensorData/id/{sensorId}/time/{timestamp}
    [HttpGet("id/{sensorId}/time/{timestamp}")]
    public ActionResult<List<SensorData>> GetSensorDataByIdAndTime(int sensorId, DateTime timestamp)
    {
        var data = _repository.GetSensorDataByIdAndTime(sensorId, timestamp);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }

    // GET /SensorData/type/{sensorType}/time/{timestamp}
    [HttpGet("type/{sensorType}/time/{timestamp}")]
    public ActionResult<List<SensorData>> GetSensorDataByTypeAndTime(string sensorType, DateTime timestamp)
    {
        var data = _repository.GetSensorDataByTypeAndTime(sensorType, timestamp);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }
}