using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColorMixer.UI.Utils
{
    public static class ScreenCapture
    {
        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(int hObject);
        
        [DllImport("gdi32.dll")]
        private static extern int CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hdc, int hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        //function to capture screen section       
        public static Bitmap CaptureScreen(int x, int y, int width, int height)
        {
            //create DC for the entire virtual screen
            int hdcSrc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            int hdcDest = CreateCompatibleDC(hdcSrc);
            int hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
            SelectObject(hdcDest, hBitmap);

            // set the destination area White - a little complicated
            Bitmap bmp = new Bitmap(width, height);
            Image ii = (Image)bmp;
            Graphics gf = Graphics.FromImage(ii);
            IntPtr hdc = gf.GetHdc();
            BitBlt(hdcDest, 0, 0, width, height, (int)hdc, 0, 0, 0x00FF0062); //use whiteness flag to make destination screen white
            gf.Dispose();
            ii.Dispose();
            bmp.Dispose();

            //Now copy the areas from each screen on the destination hbitmap
            var screens = Screen.AllScreens;
            int X, X1, Y, Y1;
            foreach (var t in screens)
            {
                if (t.Bounds.X > (x + width) || (t.Bounds.X + t.Bounds.Width) < x || t.Bounds.Y > (y + height) || (t.Bounds.Y + t.Bounds.Height) < y)
                {// no common area
                }
                else
                {
                    // something  common
                    X = x < t.Bounds.X ? t.Bounds.X : x;

                    if ((x + width) > (t.Bounds.X + t.Bounds.Width))
                    {
                        X1 = t.Bounds.X + t.Bounds.Width;
                    }
                    else
                    {
                        X1 = x + width;
                    }

                    Y = y < t.Bounds.Y ? t.Bounds.Y : y;

                    if ((y + height) > (t.Bounds.Y + t.Bounds.Height))
                    {
                        Y1 = t.Bounds.Y + t.Bounds.Height;
                    }
                    else
                    {
                        Y1 = y + height;
                    }
                    // Main API that does memory data transfer
                    BitBlt(hdcDest, X - x, Y - y, X1 - X, Y1 - Y, hdcSrc, X, Y, 0x40000000 | 0x00CC0020);
                }
            }
            
            var imf = Image.FromHbitmap(new IntPtr(hBitmap));
            DeleteDC(hdcSrc);
            DeleteDC(hdcDest);
            DeleteObject(hBitmap);

            return imf;
        }

    }
}
