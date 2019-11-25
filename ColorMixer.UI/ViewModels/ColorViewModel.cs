using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ColorMixer.UI.ViewModels
{
    public class ColorViewModel : ViewModelBase
    {
        private int m_R;
        private int m_G;
        private int m_B;
        private Color m_Color;

        public int R
        {
            get => m_R;
            set => SetProperty(ref m_R, value);
        }

        public int G
        {
            get => m_G;
            set => SetProperty(ref m_G, value);
        }

        public int B
        {
            get => m_B;
            set => SetProperty(ref m_B, value);
        }

        public Color Color
        {
            get => m_Color;
            set
            {
                SetProperty(ref m_Color, value);
                R = Color.R;
                G = Color.G;
                B = Color.B;
            }
        }
        
    }
}
