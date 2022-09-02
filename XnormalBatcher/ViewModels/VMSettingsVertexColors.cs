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
    internal class SettingsVertexColors
    {
        internal bool WriteObjectID;
        internal bool UseDrawColor;
        internal Color DrawColor;
        internal Color BackgroundColor;

        internal SettingsVertexColors()
        {
            WriteObjectID = false;
            UseDrawColor = true;
            DrawColor = Color.FromRgb(255, 0, 0);
            BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }
    internal class VMSettingsVertexColors : BaseViewModel
    {
        private SettingsVertexColors Data { get; set; }

        internal VMSettingsVertexColors()
        {
            Data = new SettingsVertexColors();
            NotifyPropertyChanged("UseDrawColor");
        }
        public bool WriteObjectID
        {
            get => Data.WriteObjectID;
            set
            {
                Data.WriteObjectID = value;
                Data.UseDrawColor = !value;
                NotifyPropertyChanged();                
            }
        }
        public bool UseDrawColor
        {
            get => Data.UseDrawColor;
            set
            {
                Data.UseDrawColor = value;
                Data.WriteObjectID = !value;
                NotifyPropertyChanged();                
            }
        }
        public Color DrawColor
        {
            get => Data.DrawColor;
            set
            {
                Data.DrawColor = value;
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
            genMaps.SetAttribute("BakeHighpolyVCols", $"{settings.BakeVertexColors}");
            XmlHelper.SetXmlColor(genMaps["BakeHighpolyVColsBackgroundCol"], BackgroundColor);
        }

    }
}
