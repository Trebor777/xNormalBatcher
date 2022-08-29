using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsHeight
    {
        internal string Normalization;
        internal Color BackgroundColor;
        internal double Minimum;
        internal double Maximum;
        internal SettingsHeight()
        {
            BackgroundColor = Color.FromRgb(0, 0, 0);
            Normalization = "Interactive";
            Minimum = 0.0;
            Maximum = 0.0;
        }
    }
    internal class VMSettingsHeight : BaseViewModel
    {
        public VMSettingsHeight()
        {
            Data = new SettingsHeight();
        }

        private SettingsHeight Data { get; set; }

        public string Normalization
        {
            get => Data.Normalization;
            set
            {
                Data.Normalization = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsManual");
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
        public double Minimum
        {
            get => Data.Minimum;
            set
            {
                Data.Minimum = value;
                NotifyPropertyChanged();
            }
        }
        public double Maximum
        {
            get => Data.Maximum;
            set
            {
                Data.Maximum = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsManual
        {
            get => Data.Normalization == "Manual";
        }
    }
}
