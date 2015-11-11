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
            CommandFactory factory = new CommandFactory();
            factory.Register("weather", new WeatherCommand() { DefaultCity = "Minsk"});

            CustomBot bot = new CustomBot();
            CommandParser parser = new CommandParser();

            while (true)
            {
                Message message = await bot.NextTextMessage();
                ICommand command = factory.GetCommand(parser.GetCommandName(message.Text));
                if (command != null)
                {
                    CommandContext context = new CommandContext(parser.GetCommandParameters(message.Text), message.Chat.Id, bot);
                    command.Execute(context);
                }
                else
                {
                    //
                }
            }
        }
    }
}


