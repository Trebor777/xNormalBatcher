using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsConvexity
    {
        internal double Scale;
        internal Color BackgroundColor;
        internal SettingsConvexity()
        {
            Scale = 1.0;
            BackgroundColor = Color.FromRgb(255, 255, 255);
        }
    }
    internal class VMSettingsConvexity : BaseViewModel
    {
        public VMSettingsConvexity()
        {
            Data = new SettingsConvexity();
        }

        private SettingsConvexity Data { get; set; }

        public double Scale
        {
            get => Data.Scale;
            set
            {
                Data.Scale = value;
                NotifyPropertyChanged();
            }
        }
        public Color BackgroundColor
        {
            get => Data.BackgroundColor;
            set
            {
                Data.BackgroundColor = value;
                NotifyPropertyChanged();
            }
        }
    }
}
