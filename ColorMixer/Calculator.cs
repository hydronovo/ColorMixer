using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ColorMixer
{
    public static class Calculator
    {
        public static Color Calculate(params ColorRatio[] colorRatios)
        {
            var ratioSum = colorRatios.Select(x => x.Ratio).Sum();
            var r = colorRatios.Select(x => x.Color.R * x.Ratio).Sum() / ratioSum;
            var g = colorRatios.Select(x => x.Color.G * x.Ratio).Sum() / ratioSum;
            var b = colorRatios.Select(x => x.Color.B * x.Ratio).Sum() / ratioSum;
            return Color.FromArgb((int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }
    }
}
