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
}