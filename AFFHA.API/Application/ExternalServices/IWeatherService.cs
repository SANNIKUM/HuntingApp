using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFFHA.API.Application.ExternalServices
{
    public interface IWeatherService
    {
        WeatherForecastDto GetWeatherInfo(string latitude, string longitude);
    }
}
