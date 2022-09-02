using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsCurvature
    {
        internal int Rays;
        internal double Bias;
        internal double SpreadAngle;
        internal double SearchDistance;
        internal bool Smoothing;
        internal bool Jitter;
        internal Color BackgroundColor;
        internal string Algorithm;
        internal string Distribution;
        internal string ToneMapping;
        internal SettingsCurvature()
        {
            Rays = 128;
            Bias = 0.0001;
            SpreadAngle = 162;
            SearchDistance = 1;
            Jitter = false;
            Smoothing = true;
            BackgroundColor = Color.FromRgb(0, 0, 0);
            Algorithm = "Average";
            Distribution = "Cosine";
            ToneMapping = "3Col";
        }
    }
    internal class VMSettingsCurvature : BaseViewModel
    {
        public VMSettingsCurvature()
        {
            Data = new SettingsCurvature();
        }

        private SettingsCurvature Data { get; set; }
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
        public bool Smoothing
        {
            get => Data.Smoothing;
            set
            {
                Data.Smoothing = value;
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
        public string Algorithm
        {
            get => Data.Algorithm;
            set
            {
                Data.Algorithm = value;
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
        public string ToneMapping
        {
            get => Data.ToneMapping;
            set
            {
                Data.ToneMapping = value;
                NotifyPropertyChanged();
            }
        }
        internal void SetXML(XmlElement genMaps, SettingsViewModel settings)
        {
            genMaps.SetAttribute("GenCurv", $"{settings.BakeCurvature}".ToLower());
            genMaps.SetAttribute("CurvRaysPerSample", $"{Rays}");
            genMaps.SetAttribute("CurvBias", $"{Bias}");
            genMaps.SetAttribute("CurvConeAngle", $"{SpreadAngle}");
            genMaps.SetAttribute("CurvSearchDistance", $"{SearchDistance}");
            genMaps.SetAttribute("CurvTonemap", $"{ToneMapping}");
            genMaps.SetAttribute("CurvDistribution", $"{Distribution}");
            genMaps.SetAttribute("CurvAlgorithm", $"{Algorithm}");
            genMaps.SetAttribute("CurvSmoothing", $"{Smoothing}".ToLower());
            genMaps.SetAttribute("CurvJitter", $"{Jitter}".ToLower());
            XmlHelper.SetXmlColor(genMaps["CurvBackgroundColor"], BackgroundColor);
        }
    }
}
