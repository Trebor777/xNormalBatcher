using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsTranslucency
    {
        internal int Rays;
        internal double Bias;
        internal double SpreadAngle;
        internal double SearchDistance;
        internal bool Jitter;
        internal string Distribution;
        internal Color BackgroundColor;

        internal SettingsTranslucency()
        {
            Rays = 128;
            Bias = 0.0005;
            SearchDistance = 1.0;
            SpreadAngle = 162.0;
            Jitter = false;
            Distribution = "Uniform";
            BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }
    
    internal class VMSettingsTranslucency : BaseViewModel
    {
        private SettingsTranslucency Data { get; set; }
        public int Rays
        {
            get => Data.Rays;
            set
            {
                Data.Rays = value;
                NotifyPropertyChanged();
            }
        }
        public double Bias
        {
            get => Data.Bias;
            set
            {
                Data.Bias = value;
                NotifyPropertyChanged();
            }
        }
        public double SpreadAngle
        {
            get => Data.SpreadAngle;
            set
            {
                Data.SpreadAngle = value;
                NotifyPropertyChanged();
            }
        }
        public double SearchDistance
        {
            get => Data.SearchDistance;
            set
            {
                Data.SearchDistance = value;
                NotifyPropertyChanged();
            }
        }
        public bool Jitter
        {
            get => Data.Jitter;
            set
            {
                Data.Jitter = value;
                NotifyPropertyChanged();
            }
        }
        public string Distribution
        {
            get => Data.Distribution;
            set
            {
                Data.Distribution = value;
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
