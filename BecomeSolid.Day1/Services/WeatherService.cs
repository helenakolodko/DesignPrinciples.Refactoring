using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Services
{
    public class WeatherService: IService
    {
        private string url = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&units=metric";
        private string apiKey;

        public WeatherService()
            : this(ConfigurationManager.AppSettings["OpenWeatherMapApiKey"])
        {
        }

        public WeatherService(string apiKey) 
        {
            this.apiKey = apiKey;
        }

        public Weather GetCityWeather(string city) 
        {
            WebUtility.UrlEncode(city);
            string requestUrl = string.Format(url, city, apiKey);
            string response = WebHelper.GetResponseString(requestUrl);
            Weather result = null;
            try
            {
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                result = new Weather()
                {
                    City = weatherResponse.Name,
                    Description = weatherResponse.Weather.First().Description,
                    Temperature = weatherResponse.Main.Temp
                };
            }
            catch { }   
            return result;
        }
    }
}


class WeatherDescription
{
    [JsonProperty("description")]
    public string Description { get; set; }
}

class Main
{

    [JsonProperty("temp")]
    public double Temp { get; set; }
}


class WeatherResponse
{

    [JsonProperty("weather")]
    public IList<WeatherDescription> Weather { get; set; }

    [JsonProperty("main")]
    public Main Main { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}