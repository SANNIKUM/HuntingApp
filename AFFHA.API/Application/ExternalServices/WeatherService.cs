using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AFFHA.API.Application.ExternalServices
{
    /// <summary>
    /// Weather Service class
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configure;
        public WeatherService(IConfiguration configure)
        {
            _configure = configure;
        }

        /// <summary>
        /// Method to get weather information based on lattitude and longitude
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        /// <returns>Weather forecast info based on lattitude and longitude</returns>
        public WeatherForecastDto GetWeatherInfo(string latitude, string longitude)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var forecastApiUrl = _configure["WeatherForecastSettings:ForcastApiUrl"];
                    var key = _configure["WeatherForecastSettings:ForcastApiKey"];
                    Uri url = new Uri(forecastApiUrl + key + "/" + latitude + "," + longitude);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body.
                        WeatherForecastDto weatherForecastDetails = response.Content.ReadAsAsync<WeatherForecastDto>().Result;
                        return weatherForecastDetails;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


