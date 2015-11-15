using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BecomeSolid.Services
{
    public class NbrbRateService : IService
    {
        private string CurrencyQueryUrl = "http://www.nbrb.by/Services/XmlExRates.aspx";
        public string GetCurrencyRate(string currency)
        {
            return null;
        }

        public async Task<Currency[]> GetCurrencyRate(IEnumerable<string> currencies)
        {
            var client = new HttpClient();
            var responseStream = new StreamReader(await client.GetStreamAsync(CurrencyQueryUrl));
            var xmlDeserializer = new XmlSerializer(typeof(DailyExRates));
            DailyExRates dailyRates = xmlDeserializer.Deserialize(responseStream) as DailyExRates;
            DailyExRatesCurrency[] rates = dailyRates.Currency;

            List<Currency> result = new List<Currency>();
            foreach (var currency in currencies)
            {
                var rate = rates.FirstOrDefault(r => String.Compare(r.CharCode, currency,true) == 0);
                if (rate != null)
                    result.Add(new Currency() { Name = rate.Name, CharCode = rate.CharCode, Rate = rate.Rate });
            }
            return result.ToArray();
        }

    }

    public class Currency
    {
        public string CharCode { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class DailyExRates
    {
        [XmlElementAttribute("Currency")]
        public DailyExRatesCurrency[] Currency { get; set; }

        [XmlAttributeAttribute()]
        public string Date { get; set; }
    }

    [XmlTypeAttribute(AnonymousType = true)]
    public partial class DailyExRatesCurrency
    {
        public ushort NumCode { get; set; }
        public string CharCode { get; set; }
        public ushort Scale { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        [XmlAttributeAttribute()]
        public ushort Id { get; set; }
    }
}
