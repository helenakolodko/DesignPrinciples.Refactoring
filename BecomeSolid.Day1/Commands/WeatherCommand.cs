using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nustache.Core;
using BecomeSolid.Services;

namespace BecomeSolid.Commands
{
    public class WeatherCommand: ICommand
    {
        private WeatherService service = new WeatherService();
        //private WeatherResponseStringBuilder builder = new WeatherResponseStringBuilder();

        public string DefaultCity { get; set; }
        public void Execute(CommandContext context)
        {
            Weather weather = GetWeather(context.Parameters);
            string response;
            if (weather != null)
                response = String.Format(new TemperatureFormatter(), "In {0} {1}, temperature is {2:C}.",
                    weather.City, weather.Description, weather.Temperature);
            else
                response = "Error.";
            context.Bot.SendTextMessage(context.ChatId, response);
        }

        private Weather GetWeather(IEnumerable<string> parameters)
        {
            Weather result;
            if (parameters.Count() == 0)
                result = service.GetCityWeather(DefaultCity);
            else
                result = service.GetCityWeather(parameters.First());
            return result;
        }

    
    }
}
