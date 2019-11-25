using System.Drawing;
using NUnit.Framework;

namespace ColorMixer.Test
{
    public class CalculatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //var colorRatio1 = new ColorRatio();
            var color1Ration = Calculator.CalculateRatio(Color.Orange, Color.DarkRed, Color.Yellow);
            var a = 1;
        }
    }
}