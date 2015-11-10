using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    public class WeatherService: IService
    {
        private string url = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}";

        public WeatherService():
            :this(ConfigurationManager.AppSettings["TelegramBotApiKey"])
        {
        }
        public WeatherService(string apiKey) 
        {
            url = String.Format(url, "{0}", apiKey);
        }

       
    }
}
