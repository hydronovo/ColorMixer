using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixer.UI.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private ColorViewModel m_TargetColor = new ColorViewModel();
        private ColorViewModel m_MixColor1 = new ColorViewModel();
        private ColorViewModel m_MixColor2 = new ColorViewModel();

        public ColorViewModel TargetColor
        {
            get => m_TargetColor;
            set => SetProperty(ref m_TargetColor, value);
        }

        public ColorViewModel MixColor1
        {
            get => m_MixColor1;
            set => SetProperty(ref m_MixColor1, value);
        }

        public ColorViewModel MixColor2
        {
            get => m_MixColor2;
            set => SetProperty(ref m_MixColor2, value);
        }
    }
}
