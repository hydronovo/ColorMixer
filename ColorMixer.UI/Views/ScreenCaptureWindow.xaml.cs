using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ColorMixer.UI.Utils;
using ColorMixer.UI.ViewModels;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Rectangle = System.Drawing.Rectangle;

namespace ColorMixer.UI.Views
{
    /// <summary>
    /// Interaction logic for ScreenCaptureWindow.xaml
    /// </summary>
    public partial class ScreenCaptureWindow : Window
    {
        private readonly Bitmap m_ScreenImage;
        private readonly Bitmap m_ScreenImageWithMargin;

        private const int m_Margin = 2;

        public ScreenCaptureWindow()
        {
            var fullScreenSize = GetFullScreenSize();
            var fullScreenSizeWithMargin = GetFullScreenSizeWithMargin(fullScreenSize, m_Margin);
            m_ScreenImage = ScreenCapture.CaptureScreen(fullScreenSize.X, fullScreenSize.Y, fullScreenSize.Width, fullScreenSize.Height);
            m_ScreenImageWithMargin = ScreenCapture.CaptureScreen(fullScreenSizeWithMargin.X, fullScreenSizeWithMargin.Y, fullScreenSizeWithMargin.Width, fullScreenSizeWithMargin.Height);

            InitializeComponent();
            SetSize(fullScreenSize);

            Background = new ImageBrush(ImageSourceFromBitmap(m_ScreenImage));
        }
        
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(handle);
            }
        }

        private static Rectangle GetFullScreenSize()
        {
            var screens = Screen.AllScreens;
            var left = screens.Min(s => s.Bounds.Left);
            var top = screens.Min(s => s.Bounds.Top);
            var right = screens.Max(s => s.Bounds.Right);
            var bottom = screens.Max(s => s.Bounds.Bottom);
            return new Rectangle(left, top, right - left, bottom - top);
        }

        private static Rectangle GetFullScreenSizeWithMargin(Rectangle original, int margin)
        {
            return new Rectangle(original.Left - margin, original.Top - margin, original.Width + 2 * margin, original.Height + 2 * margin);
        }

        private void SetSize(Rectangle fullScreen)
        {
            Left = fullScreen.Left;
            Top = fullScreen.Top;
            Height = fullScreen.Height;
            Width = fullScreen.Width;
        }
        
        private void ScreenCaptureWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_ScreenImage.Dispose();
            m_ScreenImageWithMargin.Dispose();
            
            Close();
        }

        private void ScreenCaptureWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            UpdateColorPicker();
        }

        public void UpdateColorPicker()
        {
            var mousePosition = Mouse.GetPosition(this);

            ColorPicker.SetValue(Canvas.LeftProperty, mousePosition.X - 31);
            ColorPicker.SetValue(Canvas.TopProperty, mousePosition.Y - 31);

            var colorViewModel = (ColorViewModel)ColorPicker.DataContext;
            colorViewModel.Color = m_ScreenImage.GetPixel((int)mousePosition.X, (int)mousePosition.Y);

            const int size = m_Margin * 2 + 1;
            var zoomInImage = CropImage(m_ScreenImageWithMargin, new Rectangle((int) mousePosition.X, (int) mousePosition.Y, size, size));
            
            var myBrush = new ImageBrush
            {
                Stretch = Stretch.UniformToFill,
                ImageSource = ImageSourceFromBitmap(zoomInImage),
            };
            ColorPicker.ZoomInCanvas.Background = myBrush;
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            return source.Clone(section, source.PixelFormat);
        }
    }
}
