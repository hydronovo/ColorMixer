using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorMixer.UI.ViewModels;

namespace ColorMixer.UI.Views
{
    /// <summary>
    /// Interaction logic for ColorView.xaml
    /// </summary>
    public partial class ColorView : UserControl
    {
        public ColorView()
        {
            InitializeComponent();
        }
        
        private void PickButton_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow == null)
            {
                return;
            }
            mainWindow.Hide();
            var screenCaptureWindow = new ScreenCaptureWindow();
            screenCaptureWindow.ColorPicker.DataContext = DataContext;
            screenCaptureWindow.ShowDialog();
            mainWindow.Show();
        }
    }
}
