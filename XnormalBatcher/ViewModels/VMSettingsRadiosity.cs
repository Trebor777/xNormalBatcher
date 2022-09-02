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
    internal class SettingsRadiosity
    {
        internal int Rays;
        internal double Bias;
        internal double SpreadAngle;
        internal double Contrast;
        internal bool LimitRayDistance;
        internal bool Jitter;
        internal bool EncodeOcclusion;
        internal bool AllowPureOcclusion;
        internal string CoordinateSystem;
        internal string Distribution;
        internal double AttenuationConstant;
        internal double AttenuationLinear;
        internal double AttenuationQuadratic;
        internal Color BackgroundColor;
        internal SettingsRadiosity()
        {
            Rays = 128;
            Bias = 0.08;
            SpreadAngle = 162;
            Contrast = 1;
            LimitRayDistance = false;
            Jitter = false;
            EncodeOcclusion = true;
            AllowPureOcclusion = false;
            CoordinateSystem = "AliB";
            Distribution = "Uniform";
            AttenuationConstant = 0;
            AttenuationLinear = 0;
            AttenuationQuadratic = 0;
            BackgroundColor = Color.FromRgb(0,0,0);
        }
    }
    internal class VMSettingsRadiosity : BaseViewModel
    {
        public VMSettingsRadiosity()
        {
            Data = new SettingsRadiosity();
        }

        private SettingsRadiosity Data { get; set; }
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
        public double Contrast
        {
            get => Data.Contrast;
            set
            {
                Data.Contrast = value;
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
        public bool Jitter
        {
            get => Data.Jitter;
            set
            {
                Data.Jitter = value;
                NotifyPropertyChanged();
            }
        }
        public bool EncodeOcclusion
        {
            get => Data.EncodeOcclusion;
            set
            {
                Data.EncodeOcclusion = value;
                NotifyPropertyChanged();
            }
        }
        public bool AllowPureOcclusion
        {
            get => Data.AllowPureOcclusion;
            set
            {
                Data.AllowPureOcclusion = value;
                NotifyPropertyChanged();
            }
        }
        public string CoordinateSystem
        {
            get => Data.CoordinateSystem;
            set
            {
                Data.CoordinateSystem = value;
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
        public double AttenuationConstant
        {
            get => Data.AttenuationConstant;
            set
            {
                Data.AttenuationConstant = value;
                NotifyPropertyChanged();
            }
        }
        public double AttenuationLinear
        {
            get => Data.AttenuationLinear;
            set
            {
                Data.AttenuationLinear = value;
                NotifyPropertyChanged();
            }
        }
        public double AttenuationQuadratic
        {
            get => Data.AttenuationQuadratic;
            set
            {
                Data.AttenuationQuadratic = value;
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
            genMaps.SetAttribute("GenRadiosityNormals", $"{settings.BakeRadiosity}".ToLower());
            genMaps.SetAttribute("RadiosityNormalsRaysPerSample", $"{Rays}");
            genMaps.SetAttribute("RadiosityNormalsConeAngle", $"{SpreadAngle}");
            genMaps.SetAttribute("RadiosityNormalsBias", $"{Bias}");
            genMaps.SetAttribute("RadiosityNormalsAttenConstant", $"{AttenuationConstant}");
            genMaps.SetAttribute("RadiosityNormalsAttenLinear", $"{AttenuationLinear}");
            genMaps.SetAttribute("RadiosityNormalsAttenCuadratic", $"{AttenuationQuadratic}");
            genMaps.SetAttribute("RadiosityNormalsContrast", $"{Contrast}");
            genMaps.SetAttribute("RadiosityNormalsJitter", $"{Jitter}".ToLower());
            genMaps.SetAttribute("RadiosityNormalsLimitRayDistance", $"{LimitRayDistance}".ToLower());
            genMaps.SetAttribute("RadiosityNormalsEncodeAO", $"{EncodeOcclusion}".ToLower());
            genMaps.SetAttribute("RadiosityNormalsAllowPureOcclusion", $"{AllowPureOcclusion}".ToLower());
            genMaps.SetAttribute("RadiosityNormalsDistribution", $"{Distribution}");
            genMaps.SetAttribute("RadiosityNormalsCoordSys", $"{CoordinateSystem}");
            XmlHelper.SetXmlColor(genMaps["RadNMBackgroundColor"], BackgroundColor);
        }
    }
}
