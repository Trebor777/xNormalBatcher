using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;
using System.Xml;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsVertexColors
    {
        internal Color BackgroundColor;

        internal SettingsVertexColors()
        {
            BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }
    internal class VMSettingsVertexColors : BaseViewModel
    {
        private SettingsVertexColors Data { get; set; }

        internal VMSettingsVertexColors()
        {
            Reset();
            CMDReset = new RelayCommand(Reset);
        }
        public ICommand CMDReset { get; set; }
        private void Reset()
        {
            Data = new SettingsVertexColors();
            NotifyPropertyChanged("BackgroundColor");
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
            genMaps.SetAttribute("BakeHighpolyVCols", $"{settings.BakeVertexColors}".ToLower());
            XmlHelper.SetXmlColor(genMaps["BakeHighpolyVColsBackgroundCol"], BackgroundColor);
        }

    }
}
