using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsBaseTexture
    {
        public Color BackgroundColor;
        public Color DrawColor;
        public bool WriteObjectID;
        public bool UseDrawColor;
        internal SettingsBaseTexture()
        {
            WriteObjectID = false;
            UseDrawColor = true;
            DrawColor = new Color() { A = 255, R = 255, G = 0, B = 0 };
            BackgroundColor = new Color() { A = 255, R = 0, G = 0, B = 0 };
        }
    }

    internal class VMSettingsBaseTexture : BaseViewModel
    {
        private SettingsBaseTexture Data { get; set; }

        public bool WriteObjectID
        {
            get => Data.WriteObjectID; set
            {
                Data.WriteObjectID = value;
                Data.UseDrawColor = !value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("UseDrawColor");
            }
        }
        public bool UseDrawColor
        {
            get => Data.UseDrawColor; set
            {
                Data.UseDrawColor = value;
                Data.WriteObjectID = !value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("WriteObjectID");
            }
        }
        public Color DrawColor
        {
            get => Data.DrawColor; set
            {
                Data.DrawColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color BackgroundColor
        {
            get => Data.BackgroundColor; set
            {
                Data.BackgroundColor = value;
                NotifyPropertyChanged();
            }
        }


        internal VMSettingsBaseTexture()
        {
            Data = new SettingsBaseTexture();
        }
    }
}
