using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixer.UI.Utils;

namespace ColorMixer.UI.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private ColorViewModel m_MixedColor = new ColorViewModel();
        private ColorViewModel m_CalculatedColor = new ColorViewModel();
        private ColorViewModel m_TargetColor = new ColorViewModel();
        private ColorViewModel m_Color1 = new ColorViewModel();
        private ColorViewModel m_Color2 = new ColorViewModel();
        private double m_Color1Ratio;
        private double m_Color2Ratio;
        
        public ColorViewModel TargetColor
        {
            get => m_TargetColor;
            set => SetProperty(ref m_TargetColor, value);
        }

        public ColorViewModel CalculatedColor
        {
            get => m_CalculatedColor;
            set => SetProperty(ref m_CalculatedColor, value);
        }

        public ColorViewModel MixedColor
        {
            get => m_MixedColor;
            set => SetProperty(ref m_MixedColor, value);
        }

        public ColorViewModel Color1
        {
            get => m_Color1;
            set => SetProperty(ref m_Color1, value);
        }

        public ColorViewModel Color2
        {
            get => m_Color2;
            set => SetProperty(ref m_Color2, value);
        }

        public double Color1Ratio
        {
            get => m_Color1Ratio;
            set
            {
                SetProperty(ref m_Color1Ratio, value);
                Color2Ratio = 100 - Color1Ratio;
                Calculate();
            }
        }

        public double Color2Ratio
        {
            get => m_Color2Ratio;
            set => SetProperty(ref m_Color2Ratio, value);
        }
        
        public void Calculate()
        {        
            MixedColor.Color = Calculator.Mix(new ColorRatio(Color1.Color, Color1Ratio), new ColorRatio(Color2.Color, Color2Ratio));
        }        
    }
}
