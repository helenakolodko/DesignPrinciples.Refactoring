using BecomeSolid.Commands;
using BecomeSolid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Commands
{
    public class CurrencyRateCommand: ICommand
    {
        private NbrbRateService service = new NbrbRateService();

        public async void Execute(CommandContext context)
        {
            Currency[] rates = await GetRates(context.Parameters);
            string response = "";
            if (rates != null)
                foreach (var rate in rates)
	            {
                    response += String.Format("{0} ({1}) - {2:C}\n",
                        rate.Name, rate.CharCode, rate.Rate);
	            }
            else
                response = "Error.";
            context.Bot.SendTextMessage(context.ChatId, response);
        }

        private async Task<Currency[]> GetRates(IEnumerable<string> currencies)
        {
            return await service.GetCurrencyRate(currencies);
        }
    }
}
