using System.Text;
using FarmBack.DTO;
using FarmBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorDataController : ControllerBase
{
    private readonly ISensorDataRepository _repository;

    public SensorDataController(ISensorDataRepository repository)
    {
        this._repository = repository;
    }

    // GET /SensorData
    [HttpGet]
    public IActionResult GetSensorsData(
        [FromQuery] string? filters = "{}",
        [FromQuery] string? sortBy = "",
        [FromQuery] string? order = "asc",
        [FromQuery] int limit = 0,
        [FromQuery] string? format = "json"
        )
    {
        var data = _repository.GetSensorsData(filters, sortBy, order, limit);
        
        if (data == null || data.Count == 0)
        {
            return NotFound();
        }
        
        if (format.ToLower() == "csv")
        {
            var csvData = ConvertToCSV(data);
            var bytes = Encoding.UTF8.GetBytes(csvData);
            return File(bytes, "text/csv", "sensor_data.csv");
        }
        
        return Ok(data);
    }
    
    private string ConvertToCSV(List<SensorData> data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Timestamp,SensorId,SensorType,Value,Unit");
        foreach (var item in data)
        {
            sb.AppendLine($"{item.Timestamp},{item.SensorId},{item.SensorType},{item.Value},{item.Unit}");
        }
        return sb.ToString();
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
    
    // GET /SensorData/{sensorId}/avg
    [HttpGet("/api/SensorData/{sensorId}/avg")]
    public ActionResult<SensorStats> GetSensorAverageData(int sensorId)
    {
        return _repository.GetSensorStats(sensorId);
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