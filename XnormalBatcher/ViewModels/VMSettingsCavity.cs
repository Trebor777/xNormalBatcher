using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using XnormalBatcher.ViewModels;
using System.Xml;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsCavity
    {
        internal int Rays;
        internal int Steps;
        internal double Radius;
        internal double Contrast;
        internal bool Jitter;
        internal Color BackgroundColor;
        internal SettingsCavity()
        {
            Rays = 128;
            Steps = 4;
            Radius = 0.5;
            Contrast = 1.25;
            Jitter = false;
            BackgroundColor = Color.FromRgb(255, 255, 255);

        }
    }
    internal class VMSettingsCavity : BaseViewModel
    {
        internal VMSettingsCavity()
        {
            Data = new SettingsCavity();
        }

        private SettingsCavity Data { get; set; }

        public int Rays
        {
            get => Data.Rays;
            set
            {
                Data.Rays = value;
                NotifyPropertyChanged();
            }
        }
        public int Steps
        {
            get => Data.Steps;
            set
            {
                Data.Steps = value;
                NotifyPropertyChanged();
            }
        }
        public double Radius
        {
            get => Data.Radius;
            set
            {
                Data.Radius = value;
                NotifyPropertyChanged();
            }
        }
        public double Contrast
        {
            get => Data.Contrast;
            set
            {
                Data.Contrast = value;
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
            genMaps.SetAttribute("GenCavity", $"{settings.BakeCavity}".ToLower());
            genMaps.SetAttribute("CavityRaysPerSample", $"{Rays}");
            genMaps.SetAttribute("CavityJitter", $"{Jitter}".ToLower());
            genMaps.SetAttribute("CavitySearchRadius", $"{Radius}");
            genMaps.SetAttribute("CavityContrast", $"{Contrast}");
            genMaps.SetAttribute("CavitySteps", $"{Steps}");
            XmlHelper.SetXmlColor(genMaps["CavityBackgroundColor"], BackgroundColor);

        }
    }
}
