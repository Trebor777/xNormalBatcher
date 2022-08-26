using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsAmbient
    {
        internal int Rays;
        internal double Bias;
        internal double SpreadAngle;
        internal bool LimitRayDistance;
        internal bool Jitter;
        internal bool IgnoreBackfaceHits;
        internal bool Allow100Occlusion;
        internal double AttenuationConstant;
        internal double AttenuationLinear;
        internal double AttenuationQuadratic;
        internal string Distribution;
        internal Color OccludedColor;
        internal Color UnoccludedColor;
        internal Color BackgroundColor;
        internal SettingsAmbient()
        {
            Rays = 128;
            Bias = 0.08;
            SpreadAngle = 162.0;
            LimitRayDistance = false;
            Jitter = false;
            IgnoreBackfaceHits = false;
            Allow100Occlusion = true;
            Distribution = "Uniform";
            OccludedColor = new Color() { A = 255, R = 0, G = 0, B = 0 };
            UnoccludedColor = new Color() { A = 255, R = 255, G = 255, B = 255 };
            BackgroundColor = new Color() { A = 255, R = 255, G = 255, B = 255 };
            AttenuationConstant = 1;
            AttenuationLinear = 0;
            AttenuationQuadratic = 0;
        }
    }

    internal class VMSettingsAmbient : BaseViewModel
    {
        private SettingsAmbient Data { get; set; }
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
        public bool IgnoreBackfaceHits
        {
            get => Data.IgnoreBackfaceHits;
            set
            {
                Data.IgnoreBackfaceHits = value;
                NotifyPropertyChanged();
            }
        }
        public bool Allow100Occlusion
        {
            get => Data.Allow100Occlusion;
            set
            {
                Data.Allow100Occlusion = value;
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
        public Color OccludedColor
        {
            get => Data.OccludedColor;
            set
            {
                Data.OccludedColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color UnoccludedColor
        {
            get => Data.UnoccludedColor;
            set
            {
                Data.UnoccludedColor = value;
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
        internal VMSettingsAmbient()
        {
            Data = new SettingsAmbient();
        }
    }
}
