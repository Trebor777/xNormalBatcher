using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsPRTpn
    {
        internal int Rays;
        internal double Bias;
        internal double SpreadAngle;
        internal double Threshold;
        internal bool Jitter;
        internal bool PRTColorNormalize;
        internal bool LimitRayDistance;
        internal Color BackgroundColor;
        internal SettingsPRTpn()
        {
            Rays = 128;
            Bias = 0.08;
            SpreadAngle = 162.0;
            Threshold = 0.05;
            Jitter = false;
            PRTColorNormalize = true;
            LimitRayDistance = false;
            BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }
    internal class VMSettingsPRTpn : BaseViewModel
    {
        private SettingsPRTpn Data { get; set; }
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
        public double Threshold
        {
            get => Data.Threshold;
            set
            {
                Data.Threshold = value;
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
        public bool PRTColorNormalize
        {
            get => Data.PRTColorNormalize;
            set
            {
                Data.PRTColorNormalize = value;
                NotifyPropertyChanged();
            }
        }
        public bool LimitRayDistance
        {
            get => Data.LimitRayDistance;
            set
            {
                Data.LimitRayDistance = value;
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

        internal VMSettingsPRTpn()
        {
            Data = new SettingsPRTpn();
        }
    }
}
