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
    internal class MeshSettingsLow
    {
        public string SmoothNormals;
        public double? MeshScale;
        public double? UOffset;
        public double? VOffset;
        public double? MeshFrontRayDistance;
        public double? MeshRearRayDistance;
        public string HighPolyOverrideFile;
        public string BlockerFile;
        public bool? HighPolyOverrideIsTangent;
        public bool? MatchUV;
        public bool? BatchProtection;
        public double? OffsetX;
        public double? OffsetY;
        public double? OffsetZ;

        internal MeshSettingsLow()
        {
            MeshScale = 1.0;
            UOffset = 0.0;
            VOffset = 0.0;
            MeshFrontRayDistance = 0.5;
            MeshRearRayDistance = 0.5;
            SmoothNormals = "Exported";
            HighPolyOverrideIsTangent = true;
            MatchUV = false;
            BatchProtection = false;
            OffsetX = 0.0;
            OffsetY = 0.0;
            OffsetZ = 0.0;
        }
    }
    internal class MeshSettingsLowVM : BaseViewModel
    {
        public static ObservableCollection<string> SmoothMethods = new ObservableCollection<string>() { "Exported", "Average", "Harden" };
        public BatchItemViewModel Owner;
        private MeshSettingsLow data;
        public string SmoothNormals
        {
            get => data.SmoothNormals; set
            {
                data.SmoothNormals = value;
                NotifyPropertyChanged();
            }
        }
        public double? MeshScale
        {
            get => data.MeshScale; set
            {
                data.MeshScale = value;
                NotifyPropertyChanged();
            }
        }
        public double? UOffset
        {
            get => data.UOffset; set
            {
                data.UOffset = value;
                NotifyPropertyChanged();
            }
        }
        public double? VOffset
        {
            get => data.VOffset; set
            {
                data.VOffset = value;
                NotifyPropertyChanged();
            }
        }
        public double? MeshFrontRayDistance
        {
            get => data.MeshFrontRayDistance; set
            {
                data.MeshFrontRayDistance = value;
                NotifyPropertyChanged();
            }
        }
        public double? MeshRearRayDistance
        {
            get => data.MeshRearRayDistance; set
            {
                data.MeshRearRayDistance = value;
                NotifyPropertyChanged();
            }
        }
        public string HighPolyOverrideFile
        {
            get => data.HighPolyOverrideFile; set
            {
                data.HighPolyOverrideFile = value;
                NotifyPropertyChanged();
            }
        }
        public string BlockerFile
        {
            get => data.BlockerFile; set
            {
                data.BlockerFile = value;
                NotifyPropertyChanged();
            }
        }
        public bool? HighPolyOverrideIsTangent
        {
            get => data.HighPolyOverrideIsTangent; set
            {
                data.HighPolyOverrideIsTangent = value;
                NotifyPropertyChanged();
            }
        }
        public bool? MatchUV
        {
            get => data.MatchUV; set
            {
                data.MatchUV = value;
                NotifyPropertyChanged();
            }
        }
        public bool? BatchProtection
        {
            get => data.BatchProtection; set
            {
                data.BatchProtection = value;
                NotifyPropertyChanged();
            }
        }
        public double? OffsetX
        {
            get => data.OffsetX; set
            {
                data.OffsetX = value;
                NotifyPropertyChanged();
            }
        }
        public double? OffsetY
        {
            get => data.OffsetY; set
            {
                data.OffsetY = value;
                NotifyPropertyChanged();
            }
        }
        public double? OffsetZ
        {
            get => data.OffsetZ; set
            {
                data.OffsetZ = value;
                NotifyPropertyChanged();
            }
        }

        private RelayCommand cmdBrowseHPTexture;
        private RelayCommand cmdBrowseBlocker;
        private RelayCommand cmdReset;
        public ICommand CMDBrowseHPTexture
        {
            get
            {
                cmdBrowseHPTexture = cmdBrowseHPTexture ?? new RelayCommand(BrowseHPTexture);
                return cmdBrowseHPTexture;
            }
        }
        public ICommand CMDBrowseBlocker
        {
            get
            {
                cmdBrowseBlocker = cmdBrowseBlocker ?? new RelayCommand(BrowseBlockerMesh);
                return cmdBrowseBlocker;
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

        internal MeshSettingsLowVM()
        {
            Reset();
        }

        internal MeshSettingsLowVM(MeshSettingsLowVM dataIn)
        {
            data = dataIn.data.Clone();
            NotifyDataChanged();
        }

        private void BrowseHPTexture()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = FileHelper.GenerateExtensionFilter("Image Files", SettingsViewModel.Instance.TextureFileFormats.ToList())
            };
            if (Directory.Exists(HighPolyOverrideFile))
                dlg.InitialDirectory = Path.GetDirectoryName(HighPolyOverrideFile);
            if (dlg.ShowDialog() == true)
            {
                HighPolyOverrideFile = dlg.FileName;
            }
        }
        private void BrowseBlockerMesh()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = FileHelper.GenerateExtensionFilter("All known file types", BatchViewModel.Instance.MeshFileFormats.ToList())
            };
            if (Directory.Exists(BlockerFile))
                dlg.InitialDirectory = Path.GetDirectoryName(BlockerFile);
            if (dlg.ShowDialog() == true)
            {
                BlockerFile = dlg.FileName;
            }
        }

        private void NotifyDataChanged()
        {
            NotifyPropertyChanged("SmoothNormals");
            NotifyPropertyChanged("MeshScale");
            NotifyPropertyChanged("UOffset");
            NotifyPropertyChanged("VOffset");
            NotifyPropertyChanged("MeshFrontRayDistance");
            NotifyPropertyChanged("MeshRearRayDistance");
            NotifyPropertyChanged("HighPolyOverrideFile");
            NotifyPropertyChanged("BlockerFile");
            NotifyPropertyChanged("HighPolyOverrideIsTangent");
            NotifyPropertyChanged("MatchUV");
            NotifyPropertyChanged("BatchProtection");
            NotifyPropertyChanged("OffsetX");
            NotifyPropertyChanged("OffsetY");
            NotifyPropertyChanged("OffsetZ");
        }

        private void Reset()
        {
            data = new MeshSettingsLow();
            NotifyDataChanged();
        }

        private MeshSettingsLow dataBefore;
        public void PrepareDataChange()
        {
            dataBefore = data.Clone();
        }

        public void RevertDataChange()
        {
            data = dataBefore.Clone();
            NotifyDataChanged();
        }
        public string OffsetString => $"{OffsetX};{OffsetY};{OffsetZ};";

        public void SetXml(XmlElement lowPolyMesh, string file, string cagefile)
        {

            lowPolyMesh.SetAttribute("File", file);
            lowPolyMesh.SetAttribute("CageFile", cagefile);
            var str = $"{(SmoothNormals == "Exported" ? "Use" : "")}{SmoothNormals}Normals";
            lowPolyMesh.SetAttribute("AverageNormals", str);
            lowPolyMesh.SetAttribute("MaxRayDistanceFront", MeshFrontRayDistance.ToString());
            lowPolyMesh.SetAttribute("MaxRayDistanceBack", MeshRearRayDistance.ToString());
            lowPolyMesh.SetAttribute("HighpolyNormalsOverrideTangentSpace", HighPolyOverrideIsTangent.ToString().ToLower());
            lowPolyMesh.SetAttribute("MatchUVs", MatchUV.ToString().ToLower());
            lowPolyMesh.SetAttribute("Scale", MeshScale.ToString());
            lowPolyMesh.SetAttribute("UOffset", UOffset.ToString());
            lowPolyMesh.SetAttribute("VOffset", VOffset.ToString());
            lowPolyMesh.SetAttribute("BatchProtect", BatchProtection.ToString().ToLower());
            lowPolyMesh.SetAttribute("PositionOffset", OffsetString);

            if (!string.IsNullOrEmpty(HighPolyOverrideFile))
                lowPolyMesh.SetAttribute("HighpolyNormalsOverride", HighPolyOverrideFile);
            if (!string.IsNullOrEmpty(BlockerFile))
                lowPolyMesh.SetAttribute("BlockersFile", BlockerFile);
        }
    }
}
