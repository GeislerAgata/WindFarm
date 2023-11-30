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
    // data format - 2023-11-20T17:08:19.000+00:00
    [HttpGet("time/{timestamp}")]
    public ActionResult<List<SensorData>> GetSensorsDataByTimestamp(DateTime timestamp)
    {
        var data = _repository.GetSensorsDataByTimestamp(timestamp);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }
    
    // GET /SensorData/hour/{timestamp}
    // data format - 17:08:20
    [HttpGet("hour/{time}")]
    public ActionResult<List<SensorData>> GetSensorsDataByHour(string time)
    {
        var data = _repository.GetSensorsDataByHour(time);
        if (!data.Any())
        {
            return NotFound();
        }
        return data;
    }
    
    // GET /SensorData/time/{timestamp}
    // data format - 2023-11-20
    [HttpGet("date/{date}")]
    public ActionResult<List<SensorData>> GetSensorsDataByDate(DateTime date)
    {
        var data = _repository.GetSensorsDataByDate(date);
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