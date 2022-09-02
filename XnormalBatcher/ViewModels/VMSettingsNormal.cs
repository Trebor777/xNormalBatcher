using System.Windows.Media;
using System.Xml;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsNormal
    {
        internal string SwizzleX;
        internal string SwizzleY;
        internal string SwizzleZ;
        internal bool TangentSpace;
        internal Color Color;

        internal SettingsNormal()
        {
            SwizzleX = "X+";
            SwizzleY = "Y+";
            SwizzleZ = "Z+";
            Color = new Color() { A = 255, R = 127, G = 127, B = 255 };
            TangentSpace = true;
        }
    }
    internal class VMSettingsNormal : BaseViewModel
    {
        private SettingsNormal Data { get; set; }
        internal VMSettingsNormal()
        {
            Data = new SettingsNormal();
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
        public Color Color
        {
            get => Data.Color;
            set { Data.Color = value; NotifyPropertyChanged(); }
        }
        internal void SetXML(XmlElement genMaps, SettingsViewModel settings)
        {
            genMaps.SetAttribute("GenNormals", $"{settings.BakeNormals}");
            genMaps.SetAttribute("TangentSpace", $"{TangentSpace}".ToLower());
            genMaps.SetAttribute("SwizzleX", $"{SwizzleX}");
            genMaps.SetAttribute("SwizzleY", $"{SwizzleY}");
            genMaps.SetAttribute("SwizzleZ", $"{SwizzleZ}");
            XmlHelper.SetXmlColor(genMaps["NMBackgroundColor"], Color);
        }
    }
}
