using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using XnormalBatcher.Helpers;

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
        internal VMSettingsProximity()
        {
            Data = new SettingsPRTpn();
            CMDReset = new RelayCommand(Reset);
        }
        public ICommand CMDReset { get; set; }
        private void Reset()
        {
            Data = new SettingsPRTpn();
            NotifyPropertyChanged("Rays");
            NotifyPropertyChanged("SpreadAngle");
            NotifyPropertyChanged("LimitRayDistance");
            NotifyPropertyChanged("BackgroundColor");
        }
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

        internal void SetXML(XmlElement genMaps, SettingsViewModel settings)
        {
            genMaps.SetAttribute("GenProximity", $"{settings.BakeProximity}".ToLower());
            genMaps.SetAttribute("ProximityRaysPerSample", $"{Rays}");
            genMaps.SetAttribute("ProximityConeAngle", $"{SpreadAngle}");
            genMaps.SetAttribute("ProximityLimitRayDistance", $"{LimitRayDistance}".ToLower());
            XmlHelper.SetXmlColor(genMaps["ProximityBackgroundColor"], BackgroundColor);
        }
    }
}
