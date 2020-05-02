using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seges.CvrServices.Hosting.V1.Dtos;
using Seges.CvrServices.Hosting.V1.Mapping;
using Seges.CvrServices.Logic;

namespace Seges.CvrServices.Hosting.V1.Controllers
{
    [ApiVersion("1", Deprecated = true)]
    [ApiController]
    [Route("[controller]/v{version:apiVersion}/")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherDummyService _weatherStub;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherDummyService weatherStub)
        {
            _logger = logger;
            _weatherStub = weatherStub;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeatherForecastDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var result = _weatherStub.Get().Select(Mapper.MapToDto);
            return Ok(result);
        }
    }
}
