using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    public class TemperatureFormatter : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;
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
            if (!this.Equals(formatProvider))
                return null;
            if (arg != null )
            {
                double value;
                if (double.TryParse(arg.ToString(), out value) )
                    switch (format.ToUpper())
                    {
                        case "G":
                        case "C":
                            return value.ToString("+#;-#") + "°C";
                        case "F":
                            return ToFahrenheit(value).ToString("+#;-#") + "°F";
                        case "K":
                            return ToKelvin(value).ToString("+#;-#") + "K";
                        default:
                            throw new FormatException(String.Format("'{0}' is not a valid format specifier.", format));
                    }
                else
                    throw new FormatException(String.Format("'{0}' is not a valid argument.", arg));
            }
            return string.Format(parent, "{0:" + format + "}", arg);
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
