using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
            SwizzleX = "Y+";
            SwizzleX = "Z+";
        }
    }
    internal class VMSettingsDirection : BaseViewModel
    {
        public VMSettingsDirection()
        {
            Data = new SettingsDirection();
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
    }
}
