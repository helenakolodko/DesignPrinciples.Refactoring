using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BecomeSolid.Day1;

namespace Tests
{
    [TestClass]
    public class TemperatureFormatterTest
    {
        [TestMethod]
        public void Format_NumberWithCFormatString_ReturnsCelsium()
        {
            int number = 15;
            string result = string.Format(new TemperatureFormatter(), "{0:C}", number);
            Assert.AreEqual("+15°C", result);
        }

        [TestMethod]
        public void Format_NumberWithFFormatString_ReturnsFahrenheit()
        {
            int number = 15;
            string result = string.Format(new TemperatureFormatter(), "{0:F}", number);
            Assert.AreEqual("+59°F", result);
        }

        [TestMethod]
        public void Format_NumberWithKFormatString_ReturnsKelvin()
        {
            int number = 15;
            string result = string.Format(new TemperatureFormatter(), "{0:K}", number);
            Assert.AreEqual("+288,15K", result);
        }

        [TestMethod]
        public void Format_NumberAndStringWithCFormatString_ReturnsStringWithCelsium()
        {
            int number = 15;
            string result = string.Format(new TemperatureFormatter(), "Temperature in {0} is {1:C}", "Minsk", number);
            Assert.AreEqual("Temperature in Minsk is +15°C", result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Format_NonNumericStringWithCFormatString_Fails()
        {
            string result = string.Format(new TemperatureFormatter(), "{0:C}", "Minsk");
        }
    }
}
