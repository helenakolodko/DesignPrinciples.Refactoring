using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid
{
    public class TemperatureFormatter : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;
        private string[] formats = { "C", "F", "K" };
        public TemperatureFormatter()
            : this(CultureInfo.CurrentCulture)
            { }

        public TemperatureFormatter(IFormatProvider parent)
        {
            this.parent = parent;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string result;
            if (format != null && IsApplicableTo(format.ToUpper(), arg))
            {
                double value;
                if (double.TryParse(arg.ToString(), out value))
                    result = GetFormatted(format.ToUpper(), value);
                else
                    throw new FormatException(String.Format("'{0}' is not a valid argument.", arg));
            }
            else
                result = String.Format(parent, "{0:" + format + "}", arg);
            return result;
        }

        private static string GetFormatted(string format, double value)
        {
            string result = "";
            switch (format)
            {
                case "C":
                    result = value.ToString("+#.##;-#.##") + "°C";
                    break;
                case "F":
                    result = ToFahrenheit(value).ToString("+#.##;-#.##") + "°F";
                    break;
                case "K":
                    result = ToKelvin(value).ToString("+#.##;-#.##") + "K";
                    break;
            }
            return result;
        }

        private bool IsApplicableTo(string format, object arg)
        {
            if (arg != null && formats.Contains(format))
                return true;
            else
                return false;
        }

        private static double ToKelvin(double value)
        {
            return value + 273.15;
        }

        private static double ToFahrenheit(double value)
        {
            return value * 9d / 5d + 32;
        }
    }

}
