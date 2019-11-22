using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorMixer.UI.Converters
{
    public class DrawingColorToMediaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var drawingColor = (System.Drawing.Color) value;
            return Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
