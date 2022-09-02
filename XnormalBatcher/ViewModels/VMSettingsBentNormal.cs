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
    internal class SettingsBentNormal
    {
        internal int Rays;
        internal double Bias;
        internal double SpreadAngle;
        internal bool LimitRayDistance;
        internal bool Jitter;
        internal bool TangentSpace;
        internal string SwizzleX;
        internal string SwizzleY;
        internal string SwizzleZ;
        internal Color BackgroundColor;
        internal string Distribution;
        internal SettingsBentNormal()
        {
            Rays = 128;
            Bias = 0.08;
            SpreadAngle = 162.0;
            LimitRayDistance = false;
            Jitter = false;
            TangentSpace = false;
            Distribution = "Uniform";
            SwizzleX = "X+";
            SwizzleY = "Y+";
            SwizzleZ = "Z+";
            BackgroundColor = new Color() { A = 255, R = 127, G = 127, B = 255 };
        }
    }
    internal class VMSettingsBentNormal : BaseViewModel
    {
        private SettingsBentNormal Data { get; set; }

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
        public string SwizzleX
        {
            get => Data.SwizzleX;
            set { Data.SwizzleX = value; NotifyPropertyChanged(); }
        }
        public string SwizzleY
        {
            get => Data.SwizzleY;
            set { Data.SwizzleY = value; NotifyPropertyChanged(); }
        }
        public string SwizzleZ
        {
            get => Data.SwizzleZ;
            set { Data.SwizzleZ = value; NotifyPropertyChanged(); }
        }
        public bool TangentSpace
        {
            get => Data.TangentSpace;
            set { Data.TangentSpace = value; NotifyPropertyChanged(); }
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
        internal VMSettingsBentNormal()
        {
            Data = new SettingsBentNormal();
        }
        internal void SetXML(XmlElement genMaps, SettingsViewModel settings)
        {
            genMaps.SetAttribute("GenBent", $"{settings.BakeBentNormals}".ToLower());
            genMaps.SetAttribute("BentRaysPerSample", $"{Rays}");
            genMaps.SetAttribute("BentConeAngle", $"{SpreadAngle}");
            genMaps.SetAttribute("BentBias", $"{Bias}");
            genMaps.SetAttribute("BentTangentSpace", $"{TangentSpace}".ToLower());
            genMaps.SetAttribute("BentLimitRayDistance", $"{LimitRayDistance}".ToLower());
            genMaps.SetAttribute("BentJitter", $"{Jitter}".ToLower());
            genMaps.SetAttribute("BentDistribution", $"{Distribution}");
            genMaps.SetAttribute("BentSwizzleX", $"{SwizzleX}");
            genMaps.SetAttribute("BentSwizzleY", $"{SwizzleY}");
            genMaps.SetAttribute("BentSwizzleZ", $"{SwizzleZ}");
            XmlHelper.SetXmlColor(genMaps["BentBackgroundColor"], BackgroundColor);
        }
    }
}
