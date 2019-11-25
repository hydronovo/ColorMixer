using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ColorMixer
{
    public static class Calculator
    {
        public static Color Mix(params ColorRatio[] colorRatios)
        {
            var ratioSum = colorRatios.Select(x => x.Ratio).Sum();
            var r = colorRatios.Select(x => x.Color.R * x.Ratio).Sum() / ratioSum;
            var g = colorRatios.Select(x => x.Color.G * x.Ratio).Sum() / ratioSum;
            var b = colorRatios.Select(x => x.Color.B * x.Ratio).Sum() / ratioSum;
            return Color.FromArgb((int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }

        public static double Deviation(Color target, Color mixedColor)
        {
            return Math.Abs(Math.Pow(target.R - mixedColor.R, 2) + Math.Pow(target.G - mixedColor.G, 2) + Math.Pow(target.B - mixedColor.B, 2));
        }

        public static double CalculateRatio(Color target, Color color1, Color color2)
        {
            var list = new List<Tuple<int, double>>();
            const int ratioSteps = 256;
            for (var i = 0; i <= ratioSteps; i++)
            {
                var mixedColor = Mix(new ColorRatio(color1, i), new ColorRatio(color2, ratioSteps - i));
                var deviation = Deviation(target, mixedColor);
                list.Add(new Tuple<int, double>(i, deviation));
                Debug.WriteLine($"{i},{deviation}");
                Console.WriteLine($"{i},{deviation}");
            }

            return (double)list.OrderBy(x => x.Item2).First().Item1 / ratioSteps;
        }

    }
}
