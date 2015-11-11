using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    class WeatherResponseStringBuilder
    {
        private string result = "In {0} {1}. Temperature {2}";
        public WeatherResponseStringBuilder InCity(string city)
        {
            return this;
        }

        public WeatherResponseStringBuilder AddTemperature(double temperature)
        {
            return this;
        }

        public WeatherResponseStringBuilder AddDescription(string description)
        {
            return this;
        }
    }
}
