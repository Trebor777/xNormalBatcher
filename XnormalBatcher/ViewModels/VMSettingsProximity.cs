using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsProximity
    {
        internal int Rays;
        internal double SpreadAngle;
        internal bool LimitRayDistance;
        internal Color BackgroundColor;
        internal SettingsProximity()
        {
            Rays = 128;
            LimitRayDistance = true;
            SpreadAngle = 80;
            BackgroundColor = Color.FromRgb(255, 255, 255);
        }
    }
    internal class VMSettingsProximity : BaseViewModel
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
        public double SpreadAngle
        {
            get => Data.SpreadAngle;
            set
            {
                Data.SpreadAngle = value;
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

        internal VMSettingsProximity()
        {
            Data = new SettingsPRTpn();
        }
    }
}
