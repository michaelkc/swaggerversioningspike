using Seges.CvrServices.Logic;
using Seges.CvrServices.Hosting.V1.Dtos;

namespace Seges.CvrServices.Hosting.V1.Mapping
{
    internal class Mapper
    {
        public static WeatherForecastDto MapToDto(WeatherForecast entity) => 
            new WeatherForecastDto 
            { 
                Date = entity.Date, 
                Summary = entity.Summary, 
                TemperatureC = entity.TemperatureC 
            };
    }
}
