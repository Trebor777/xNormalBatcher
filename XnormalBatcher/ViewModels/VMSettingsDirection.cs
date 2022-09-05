using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;
using System.Xml;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsDirection
    {
        internal bool TangentSpace;
        internal string SwizzleX;
        internal string SwizzleY;
        internal string SwizzleZ;
        internal Color BackgroundColor;
        internal string ToneMap;
        internal double ToneMinimum;
        internal double ToneMaximum;
        internal SettingsDirection()
        {
            TangentSpace = true;
            BackgroundColor = Color.FromRgb(0, 0, 0);
            ToneMap = "Interactive";
            ToneMinimum = 0;
            ToneMaximum = 100.0;
            SwizzleX = "X+";
            SwizzleY = "Y+";
            SwizzleZ = "Z+";
        }
    }
    internal class VMSettingsDirection : BaseViewModel
    {
        public VMSettingsDirection()
        {
            Data = new SettingsDirection();
            CMDReset = new RelayCommand(Reset);
        }
        public ICommand CMDReset { get; set; }
        private void Reset()
        {
            Data = new SettingsDirection();
            NotifyPropertyChanged("ToneMap");
            NotifyPropertyChanged("ToneMaximum");
            NotifyPropertyChanged("TangentSpace");
            NotifyPropertyChanged("ToneMinimum");
            NotifyPropertyChanged("SwizzleX");
            NotifyPropertyChanged("SwizzleY");
            NotifyPropertyChanged("SwizzleZ");
            NotifyPropertyChanged("BackgroundColor");
        }
        private SettingsDirection Data { get; set; }
        public bool TangentSpace
        {
            get => Data.TangentSpace;
            set
            {
                Data.TangentSpace = value;
                NotifyPropertyChanged();
            }
        }
        public string SwizzleX
        {
            get => Data.SwizzleX;
            set
            {
                Data.SwizzleX = value;
                NotifyPropertyChanged();
            }
        }
        public string SwizzleY
        {
            get => Data.SwizzleY;
            set
            {
                Data.SwizzleY = value;
                NotifyPropertyChanged();
            }
        }
        public string SwizzleZ
        {
            get => Data.SwizzleZ;
            set
            {
                Data.SwizzleZ = value;
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
        public string ToneMap
        {
            get => Data.ToneMap;
            set
            {
                Data.ToneMap = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsManual");
            }
        }
        public double ToneMinimum
        {
            get => Data.ToneMinimum;
            set
            {
                Data.ToneMinimum = value;
                NotifyPropertyChanged();
            }
        }
        public double ToneMaximum
        {
            get => Data.ToneMaximum;
            set
            {
                Data.ToneMaximum = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsManual
        {
            get => Data.ToneMap == "Manual";
        }
        internal void SetXML(XmlElement genMaps, SettingsViewModel settings)
        {
            genMaps.SetAttribute("GenDirections", $"{settings.BakeDirection}".ToLower());
            genMaps.SetAttribute("DirectionsTS", $"{TangentSpace}".ToLower());
            genMaps.SetAttribute("DirectionsSwizzleX", $"{SwizzleX}");
            genMaps.SetAttribute("DirectionsSwizzleY", $"{SwizzleY}");
            genMaps.SetAttribute("DirectionsSwizzleZ", $"{SwizzleZ}");
            genMaps.SetAttribute("DirectionsTonemap", $"{ToneMap}");
            genMaps.SetAttribute("DirectionsTonemapMin", IsManual ? $"{ToneMinimum}" : "false");
            genMaps.SetAttribute("DirectionsTonemapMax", IsManual ? $"{ToneMaximum}" : "false");
            XmlHelper.SetXmlColor(genMaps["VDMBackgroundColor"], BackgroundColor);
        }
    }
}
