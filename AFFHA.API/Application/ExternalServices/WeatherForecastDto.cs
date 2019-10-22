using System.Collections.Generic;

namespace AFFHA.API.Application.ExternalServices
{
    public class WeatherForecastDto
    {
        public string Latitude { get; set; }
        public string longitude { get; set; }
        public string Timezone { get; set; }
        public CurrentWeather Currently { get; set; }
        public DailyWeather Daily { get; set; }
    }

    public class CurrentWeather
    {
        public string Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
        public string Temperature { get; set; }
    }

    public class DailyWeather
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public List<DailyWeatherData> Data { get; set; }
    }

    public class DailyWeatherData
    {
        public string Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
        public string SunriseTime { get; set; }
        public string SunsetTime { get; set; }
    }
}
