using System;
using System.Drawing;

namespace ColorMixer
{
    public class ColorRatio
    {
        public ColorRatio(Color color, double ratio)
        {
            Color = color;
            Ratio = ratio;
        }

        public Color Color { get; }
        public double Ratio { get; }
    }
}
