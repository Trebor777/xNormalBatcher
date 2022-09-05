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
        internal VMSettingsBaseTexture()
        {
            Data = new SettingsBaseTexture();
            CMDReset = new RelayCommand(Reset);
        }

        public ICommand CMDReset { get; set; }

        private void Reset()
        {
            Data = new SettingsBaseTexture();
            NotifyPropertyChanged("WriteObjectID");
            NotifyPropertyChanged("DrawColor");
            NotifyPropertyChanged("BackgroundColor");
            NotifyPropertyChanged("UseDrawColor");
        }
        public bool WriteObjectID
        {
            get => Data.WriteObjectID; set
            {
                Data.WriteObjectID = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("UseDrawColor");
            }
        }
        public bool UseDrawColor
        {
            get => !Data.WriteObjectID; set
            {
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
        internal void SetXML(XmlElement genMaps, SettingsViewModel settings)
        {
            genMaps.SetAttribute("BakeHighpolyBaseTex", $"{settings.BakeBaseTexture}".ToLower());
            genMaps.SetAttribute("BakeHighpolyBaseTextureDrawObjectIDIfNoTexture", $"{WriteObjectID}".ToLower());
            XmlHelper.SetXmlColor(genMaps["BakeHighpolyBaseTextureNoTexCol"], DrawColor);
            XmlHelper.SetXmlColor(genMaps["BakeHighpolyBaseTextureBackgroundColor"], BackgroundColor);
        }
    }
}
