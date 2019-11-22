using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorMixer.UI.ViewModels;
using ColorMixer.UI.Utils;
using ColorMixer.UI.Views;
using Microsoft.Win32;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace ColorMixer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel m_ViewModel;

        public MainWindow()
        {
            InitializeComponent();
            m_ViewModel = new ViewModel();
            DataContext = m_ViewModel;
        }
        
    }
}
