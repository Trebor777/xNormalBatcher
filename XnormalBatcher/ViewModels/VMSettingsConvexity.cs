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
    internal class SettingsConvexity
    {
        internal double Scale;
        internal Color BackgroundColor;
        internal SettingsConvexity()
        {
            Scale = 1.0;
            BackgroundColor = Color.FromRgb(255, 255, 255);
        }
    }
    internal class VMSettingsConvexity : BaseViewModel
    {
        public VMSettingsConvexity()
        {
            Data = new SettingsConvexity();
            CMDReset = new RelayCommand(Reset);
        }
        public ICommand CMDReset { get; set; }
        private void Reset()
        {
            Data = new SettingsConvexity();
            NotifyPropertyChanged("Scale");            
            NotifyPropertyChanged("BackgroundColor");
        }
        private SettingsConvexity Data { get; set; }

        public double Scale
        {
            get => Data.Scale;
            set
            {
                Data.Scale = value;
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
            genMaps.SetAttribute("GenConvexity", $"{settings.BakeCurvature}".ToLower());
            genMaps.SetAttribute("ConvexityScale", $"{Scale}");
            XmlHelper.SetXmlColor(genMaps["ConvexityBackgroundColor"], BackgroundColor);
        }
    }
}
