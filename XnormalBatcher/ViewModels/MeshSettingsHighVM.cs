using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    [Serializable]
    internal class MeshSettingsHigh
    {
        internal double MeshScale;
        internal string BaseTexture;
        internal string SmoothNormals;
        internal bool BaseTextureIsTangent;
        internal bool IgnoreVertexColor;
        internal double OffsetX;
        internal double OffsetY;
        internal double OffsetZ;

        internal MeshSettingsHigh()
        {
            MeshScale = 1.0;
            BaseTextureIsTangent = false;
            IgnoreVertexColor = false;
            SmoothNormals = "Exported";
            OffsetX = 0.0;
            OffsetY = 0.0;
            OffsetZ = 0.0;
        }

        internal MeshSettingsHigh DeepCopy()
        {
            return new MeshSettingsHigh() {
                MeshScale = MeshScale,
                BaseTextureIsTangent = BaseTextureIsTangent,
                IgnoreVertexColor = IgnoreVertexColor,
                SmoothNormals = SmoothNormals,
                OffsetX = OffsetX,
                OffsetY = OffsetY,
                OffsetZ = OffsetZ
            };
        }
    }

    internal class MeshSettingsHighVM : BaseViewModel
    {
        public static ObservableCollection<string> SmoothMethods = new ObservableCollection<string>() { "Exported", "Average", "Harden" };
        public BatchItemViewModel Owner;
        private MeshSettingsHigh data;
        public double MeshScale
        {
            get => data.MeshScale; set
            {
                data.MeshScale = value;
                NotifyPropertyChanged();
            }
        }
        public string BaseTexture
        {
            get => data.BaseTexture; set
            {
                data.BaseTexture = value;
                NotifyPropertyChanged();
            }
        }
        public string SmoothNormals
        {
            get => data.SmoothNormals; set
            {
                data.SmoothNormals = value;
                NotifyPropertyChanged();
            }
        }
        public bool BaseTextureIsTangent
        {
            get => data.BaseTextureIsTangent; set
            {
                data.BaseTextureIsTangent = value;
                NotifyPropertyChanged();
            }
        }
        public bool IgnoreVertexColor
        {
            get => data.IgnoreVertexColor; set
            {
                data.IgnoreVertexColor = value;
                NotifyPropertyChanged();
            }
        }
        public double OffsetX
        {
            get => data.OffsetX; set
            {
                data.OffsetX = value;
                NotifyPropertyChanged();
            }
        }
        public double OffsetY
        {
            get => data.OffsetY; set
            {
                data.OffsetY = value;
                NotifyPropertyChanged();
            }
        }
        public double OffsetZ
        {
            get => data.OffsetZ; set
            {
                data.OffsetZ = value;
                NotifyPropertyChanged();
            }
        }

        private RelayCommand cmdBrowse;
        private RelayCommand cmdReset;
        public ICommand CMDBrowseBaseTexture
        {
            get
            {
                cmdBrowse = cmdBrowse ?? new RelayCommand(BrowseBaseTexture);
                return cmdBrowse;
            }
        }

        public ICommand CMDReset
        {
            get
            {
                cmdReset = cmdReset ?? new RelayCommand(Reset);
                return cmdReset;
            }
        }

        internal MeshSettingsHighVM()
        {
            Reset();
        }

        internal MeshSettingsHighVM(MeshSettingsHighVM dataIn)
        {
            data = dataIn.data.DeepCopy();
        }

        private void BrowseBaseTexture()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = FileHelper.GenerateExtensionFilter("Image Files", SettingsViewModel.Instance.TextureFileFormats.ToList())
            };
            if (File.Exists(BaseTexture))
                dlg.InitialDirectory = Path.GetDirectoryName(BaseTexture);
            if (dlg.ShowDialog() == true)
            {
                BaseTexture = dlg.FileName;
            }
        }
        private void Reset()
        {
            data = new MeshSettingsHigh();
            NotifyDataChanged();
        }

        private void NotifyDataChanged()
        {
            NotifyPropertyChanged("MeshScale");
            NotifyPropertyChanged("BaseTexture");
            NotifyPropertyChanged("SmoothNormals");
            NotifyPropertyChanged("BaseTextureIsTangent");
            NotifyPropertyChanged("IgnoreVertexColor");
            NotifyPropertyChanged("OffsetX");
            NotifyPropertyChanged("OffsetY");
            NotifyPropertyChanged("OffsetZ");
        }

        private MeshSettingsHigh dataBefore;
        public void PrepareDataChange() // Clone current data state, before any modifications from dialog
        {
            dataBefore = data.DeepCopy();
        }

        public void RevertDataChange() // Revert data state, after any modifications from dialog when cancelling.
        {
            data = dataBefore.DeepCopy();
            NotifyDataChanged();
        }

        public string OffsetString => $"{OffsetX};{OffsetY};{OffsetZ};";

        public void AppendToXML(XmlElement parent, XmlDocument root, string meshFile)
        {
            var child = root.CreateElement("Mesh");
            child.SetAttribute("Visible", "true");
            child.SetAttribute("Scale", MeshScale.ToString());
            child.SetAttribute("IgnorePerVertexColor", IgnoreVertexColor.ToString().ToLower());
            child.SetAttribute("AverageNormals", (SmoothNormals == "Exported" ? "Use" : "") + SmoothNormals + "Normals");
            child.SetAttribute("BaseTex", BaseTexture);
            child.SetAttribute("BaseTexIsTSNM", BaseTextureIsTangent.ToString().ToLower());
            child.SetAttribute("File", meshFile);
            child.SetAttribute("PositionOffset", OffsetString);
            parent.AppendChild(child);
        }

    }
}
