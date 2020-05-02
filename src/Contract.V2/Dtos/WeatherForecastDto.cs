using System;

namespace Seges.CvrServices.Contract.V2
{
    internal class WeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
