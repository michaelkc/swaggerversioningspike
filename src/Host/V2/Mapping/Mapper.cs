using Seges.CvrServices.Hosting.V2.Dtos;
using Seges.CvrServices.Logic;

namespace Seges.CvrServices.Hosting.V2.Mapping
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
