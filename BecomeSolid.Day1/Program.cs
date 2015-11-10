using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;
using Newtonsoft.Json;
using System.Configuration;

namespace BecomeSolid.Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        static async Task Run()
        {

            CustomBot bot = new CustomBot();
            var weatherApiKey = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            

            while (true)
            {
                Message message = await bot.NextTextMessage();
                             
                var inputMessage = message.Text;
                if (inputMessage.StartsWith("/weather"))
                {
                    var messageParts = inputMessage.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var city = messageParts.Length == 1 ? "Minsk" : messageParts.Skip(1).First();
                    WebUtility.UrlEncode(city);
                    string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&units=metric", city, weatherApiKey);
                    WebRequest request = WebRequest.Create(url);
                    
                        WebResponse response = request.GetResponse();
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseString = streamReader.ReadToEnd();

                            Console.WriteLine(responseString);
                            string outMessage;
                            try
                            {
                                WeatherResponse jsonResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseString);
                                Weather weather = jsonResponse.Weather.First();
                                double temp = jsonResponse.Main.Temp;
                                string description = weather.Description;
                                string cityName = jsonResponse.Name;                        
                                outMessage = "In " + cityName + " " + description + " and the temperature is " + temp.ToString("+#;-#") + "°C";
                            }
                            catch 
                            {
                                outMessage = "Error: City " + city + " is not found";
                            }
                            await bot.SendTextMessage(message.Chat.Id, outMessage);
                            Console.WriteLine("Echo Message: {0}", message);
                        }
                    
                }
                else 
                {
                    await bot.SendChatAction(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(2000);
                    await bot.SendTextMessage(message.Chat.Id, message.Text);
                    Console.WriteLine("Echo Message: {0}", message.Text);
                }

                await Task.Delay(1000);
            }
        }
    }
}








public class MessageService
{
    private Api bot;
    public MessageService(Api bot)
    {
        this.bot = bot;
    }

}


public class WeatherStringBuilder
{
    private string result;
    public WeatherStringBuilder InCity(string city)
    {
        return this;
    }

    public WeatherStringBuilder AddTemperature(double temperature)
    {
        return this;
    }

    public WeatherStringBuilder AddDescription(string description)
    {
        return this;
    }
}


public class Weather
{
    [JsonProperty("description")]
    public string Description { get; set; }
}

public class Main
{

    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("pressure")]
    public double Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }
}


public class WeatherResponse
{

    [JsonProperty("weather")]
    public IList<Weather> Weather { get; set; }

    [JsonProperty("main")]
    public Main Main { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}