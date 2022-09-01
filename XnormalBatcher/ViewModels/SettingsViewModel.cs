using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace XnormalBatcher.ViewModels
{
    // Holds information/logic on bake settings...
    [JsonObject(MemberSerialization.OptIn)]
    internal class SettingsModel
    {
        internal static List<int> AASizes = new List<int>() { 1, 2, 4 };
        internal static List<int> BucketSizes = new List<int>() { 16, 32, 64, 128, 256, 512 };
        internal static List<string> TextureFileFormats = new List<string>() { "tga", "tif", "tiff", "jpg", "png", "raw", "dds", "j2k", "wdp", "jxr", "hdp", "hdr", "imgj", "webp", "exr" };
        internal static List<string> Axis = new List<string>() { "X+", "X-", "Y+", "Y-", "Z+", "Z-" };
        internal static List<string> Distributions = new List<string>() { "Uniform", "Cosine", "CosineSq" };
        internal static List<string> Algorithms = new List<string>() { "Average", "Gaussian" };
        internal static List<string> CoordSystems = new List<string>() { "OpenGL", "DirectX", "AliB" };
        internal static List<string> ToneMappings = new List<string>() { "Monocrome", "2Col", "3Col" };
        internal static List<string> Normalizations = new List<string>() { "Interactive", "Manual", "Raw FP Values" };

        public SettingsModel()
        {
            EdgePadding = 16;
            BakingPath = @"E:\bake\";
            BakeNormals = true;
            BakeAmbient = true;
            BakeHeight = false;
            BakeCurvature = false;
            BakeVertexColors = false;
            BakeWireframe = false;
            BakeTranslucency = false;
            BakeRadiosity = false;
            BakePRTpn = false;
            BakeProximity = false;
            BakeDirection = false;
            BakeDerivative = false;
            BakeConvexity = false;
            BakeCavity = false;
            BakeBentNormals = false;
            BakeBaseTexture = false;
            BakeThickness = false;
            SelectedAASize = AASizes[0];
            SelectedBucketSize = BucketSizes[2];
            SelectedTextureFileFormat = TextureFileFormats[4];
            SettingsAmbient = new VMSettingsAmbient();
            SettingsBaseTexture = new VMSettingsBaseTexture();
            SettingsBentNormal = new VMSettingsBentNormal();
            SettingsCavity = new VMSettingsCavity();
            SettingsConvexity = new VMSettingsConvexity();
            SettingsCurvature = new VMSettingsCurvature();
            SettingsDerivative = new VMSettingsDerivative();
            SettingsDirection = new VMSettingsDirection();
            SettingsHeight = new VMSettingsHeight();
            SettingsNormal = new VMSettingsNormal();
            SettingsProximity = new VMSettingsProximity();
            SettingsPRTpn = new VMSettingsPRTpn();
            SettingsRadiosity = new VMSettingsRadiosity();
            SettingsTranslucency = new VMSettingsTranslucency();
            SettingsVertexColors = new VMSettingsVertexColors();
            SettingsWireframe = new VMSettingsWireframe();
        }
        [JsonProperty]
        public int EdgePadding { get; set; }
        [JsonProperty]
        public string XNormalPath { get; set; }
        [JsonProperty]
        public string BakingPath { get; set; }
        [JsonProperty]
        public int SelectedAASize { get; set; }
        [JsonProperty]
        public int SelectedBucketSize { get; set; }
        [JsonProperty]
        public string SelectedTextureFileFormat { get; set; }
        [JsonProperty]
        public bool BakeNormals { get; set; }
        [JsonProperty]
        public bool BothNormalsType { get; set; }
        [JsonProperty]
        public bool BakeAmbient { get; set; }
        [JsonProperty]
        public bool BakeHeight { get; set; }
        [JsonProperty]
        public bool BakeCurvature { get; set; }
        [JsonProperty]
        public bool BakeVertexColors { get; set; }
        [JsonProperty]
        public bool BakeWireframe { get; set; }
        [JsonProperty]
        public bool BakeTranslucency { get; set; }
        [JsonProperty]
        public bool BakeRadiosity { get; set; }
        [JsonProperty]
        public bool BakePRTpn { get; set; }
        [JsonProperty]
        public bool BakeProximity { get; set; }
        [JsonProperty]
        public bool BakeDirection { get; set; }
        [JsonProperty]
        public bool BakeDerivative { get; set; }
        [JsonProperty]
        public bool BakeConvexity { get; set; }
        [JsonProperty]
        public bool BakeCavity { get; set; }
        [JsonProperty]
        public bool BakeBentNormals { get; set; }
        [JsonProperty]
        public bool BakeBaseTexture { get; set; }
        [JsonProperty]
        public bool BakeThickness { get; set; }
        [JsonProperty]
        public bool ClosestHitRayFails { get; set; }
        [JsonProperty]
        public bool DiscardBackFaceHit { get; set; }
        [JsonProperty]
        public VMSettingsAmbient SettingsAmbient { get; set; }
        [JsonProperty]
        public VMSettingsBaseTexture SettingsBaseTexture { get; set; }
        [JsonProperty]
        public VMSettingsBentNormal SettingsBentNormal { get; set; }
        [JsonProperty]
        public VMSettingsCavity SettingsCavity { get; set; }
        [JsonProperty]
        public VMSettingsConvexity SettingsConvexity { get; set; }
        [JsonProperty]
        public VMSettingsCurvature SettingsCurvature { get; set; }
        [JsonProperty]
        public VMSettingsDerivative SettingsDerivative { get; set; }
        [JsonProperty]
        public VMSettingsDirection SettingsDirection { get; set; }
        [JsonProperty]
        public VMSettingsHeight SettingsHeight { get; set; }
        [JsonProperty]
        public VMSettingsNormal SettingsNormal { get; set; }
        [JsonProperty]
        public VMSettingsProximity SettingsProximity { get; set; }
        [JsonProperty]
        public VMSettingsPRTpn SettingsPRTpn { get; set; }
        [JsonProperty]
        public VMSettingsRadiosity SettingsRadiosity { get; set; }
        [JsonProperty]
        public VMSettingsTranslucency SettingsTranslucency { get; set; }
        [JsonProperty]
        public VMSettingsVertexColors SettingsVertexColors { get; set; }
        [JsonProperty]
        public VMSettingsWireframe SettingsWireframe { get; set; }
    }
    internal class SettingsViewModel : BaseViewModel
    {
        public static SettingsViewModel Instance { get; } = new SettingsViewModel();
        internal SettingsModel Data;
        #region Privates        
        private ObservableCollection<int> aaSizes;
        private ObservableCollection<int> bucketSizes;
        private ObservableCollection<string> textureFileFormats;
        private ObservableCollection<string> axis;
        private ObservableCollection<string> distributions;
        private ObservableCollection<string> algorithms;
        private ObservableCollection<string> coordSystems;
        private ObservableCollection<string> toneMappings;
        private ObservableCollection<string> normalizations;
        #endregion
        #region Properties
        // Show the Settings Panel depending if any map to bake is checked. Else Hide it. Uses a BooleanToVisibilityConverter
        public bool BakeSettingsVisibility => BakeNormals || BakeAmbient || BakeHeight || BakeCurvature || BakeVertexColors
                    || BakeWireframe || BakeTranslucency || BakeRadiosity
                    || BakePRTpn || BakeProximity || BakeDirection || BakeDerivative || BakeConvexity
                    || BakeCavity || BakeBentNormals || BakeBaseTexture;
        public ICommand BrowseXnormal { get; set; }
        public ICommand BrowseBakingPath { get; set; }
        public ICommand CMDQuickBakes { get; set; }
        public ICommand CMDResetALL { get; set; }
        [JsonProperty]
        public int EdgePadding
        {
            get => Data.EdgePadding; set
            {
                Data.EdgePadding = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public string XNormalPath
        {
            get => Data.XNormalPath; set
            {
                Data.XNormalPath = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public string BakingPath
        {
            get => Data.BakingPath; set
            {
                Data.BakingPath = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<int> AASizes
        {
            get => aaSizes; set
            {
                aaSizes = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<int> BucketSizes
        {
            get => bucketSizes; set
            {
                bucketSizes = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> TextureFileFormats
        {
            get => textureFileFormats; set
            {
                textureFileFormats = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> Axis
        {
            get => axis; set
            {
                axis = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> Distributions
        {
            get => distributions; set
            {
                distributions = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> Algorithms
        {
            get => algorithms; set
            {
                algorithms = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> CoordSystems
        {
            get => coordSystems; set
            {
                coordSystems = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> ToneMappings
        {
            get => toneMappings; set
            {
                toneMappings = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<string> Normalizations
        {
            get => normalizations;
            set
            {
                normalizations = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public int SelectedAASize
        {
            get => Data.SelectedAASize; set
            {
                Data.SelectedAASize = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public int SelectedBucketSize
        {
            get => Data.SelectedBucketSize; set
            {
                Data.SelectedBucketSize = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public string SelectedTextureFileFormat
        {
            get => Data.SelectedTextureFileFormat; set
            {
                Data.SelectedTextureFileFormat = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeNormals
        {
            get => Data.BakeNormals; set
            {
                Data.BakeNormals = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BothNormalsType
        {
            get => Data.BothNormalsType; set
            {
                Data.BothNormalsType = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeAmbient
        {
            get => Data.BakeAmbient; set
            {
                Data.BakeAmbient = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeHeight
        {
            get => Data.BakeHeight; set
            {
                Data.BakeHeight = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeCurvature
        {
            get => Data.BakeCurvature; set
            {
                Data.BakeCurvature = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeVertexColors
        {
            get => Data.BakeVertexColors; set
            {
                Data.BakeVertexColors = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeThickness
        {
            get => Data.BakeThickness; set
            {
                Data.BakeThickness = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeCavity
        {
            get => Data.BakeCavity; set
            {
                Data.BakeCavity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeBentNormals
        {
            get => Data.BakeBentNormals; set
            {
                Data.BakeBentNormals = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeDirection
        {
            get => Data.BakeDirection; set
            {
                Data.BakeDirection = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeConvexity
        {
            get => Data.BakeConvexity; set
            {
                Data.BakeConvexity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeDerivative
        {
            get => Data.BakeDerivative; set
            {
                Data.BakeDerivative = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeRadiosity
        {
            get => Data.BakeRadiosity; set
            {
                Data.BakeRadiosity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeProximity
        {
            get => Data.BakeProximity; set
            {
                Data.BakeProximity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakePRTpn
        {
            get => Data.BakePRTpn; set
            {
                Data.BakePRTpn = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeBaseTexture
        {
            get => Data.BakeBaseTexture; set
            {
                Data.BakeBaseTexture = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeWireframe
        {
            get => Data.BakeWireframe; set
            {
                Data.BakeWireframe = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeTranslucency
        {
            get => Data.BakeTranslucency; set
            {
                Data.BakeTranslucency = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool ClosestHitRayFails
        {
            get => Data.ClosestHitRayFails; set
            {
                Data.ClosestHitRayFails = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool DiscardBackFaceHit
        {
            get => Data.DiscardBackFaceHit; set
            {
                Data.DiscardBackFaceHit = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsAmbient SettingsAmbient
        {
            get => Data.SettingsAmbient; set
            {
                Data.SettingsAmbient = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsBaseTexture SettingsBaseTexture
        {
            get => Data.SettingsBaseTexture; set
            {
                Data.SettingsBaseTexture = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsBentNormal SettingsBentNormal
        {
            get => Data.SettingsBentNormal; set
            {
                Data.SettingsBentNormal = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsCavity SettingsCavity
        {
            get => Data.SettingsCavity; set
            {
                Data.SettingsCavity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsConvexity SettingsConvexity
        {
            get => Data.SettingsConvexity; set
            {
                Data.SettingsConvexity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsCurvature SettingsCurvature
        {
            get => Data.SettingsCurvature; set
            {
                Data.SettingsCurvature = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsDerivative SettingsDerivative
        {
            get => Data.SettingsDerivative; set
            {
                Data.SettingsDerivative = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsDirection SettingsDirection
        {
            get => Data.SettingsDirection; set
            {
                Data.SettingsDirection = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsHeight SettingsHeight
        {
            get => Data.SettingsHeight; set
            {
                Data.SettingsHeight = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsNormal SettingsNormal
        {
            get => Data.SettingsNormal; set
            {
                Data.SettingsNormal = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsProximity SettingsProximity
        {
            get => Data.SettingsProximity; set
            {
                Data.SettingsProximity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsPRTpn SettingsPRTpn
        {
            get => Data.SettingsPRTpn; set
            {
                Data.SettingsPRTpn = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsRadiosity SettingsRadiosity
        {
            get => Data.SettingsRadiosity; set
            {
                Data.SettingsRadiosity = value;
                NotifyPropertyChanged();
            }
        }
        /*
        public VMSettingsThickness SettingsThickness
        {
            get => settingsThickness; set
            {
                settingsThickness = value;
                NotifyPropertyChanged();
            }
        }*/
        [JsonProperty]
        public VMSettingsTranslucency SettingsTranslucency
        {
            get => Data.SettingsTranslucency; set
            {
                Data.SettingsTranslucency = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsVertexColors SettingsVertexColors
        {
            get => Data.SettingsVertexColors; set
            {
                Data.SettingsVertexColors = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsWireframe SettingsWireframe
        {
            get => Data.SettingsWireframe; set
            {
                Data.SettingsWireframe = value;
                NotifyPropertyChanged();
            }
        }
#endregion
        private SettingsViewModel()
        {
            Data = MainViewModel.LastSession?.Settings ?? new SettingsModel();
            RefreshData();
            AASizes = new ObservableCollection<int>(SettingsModel.AASizes);
            BucketSizes = new ObservableCollection<int>(SettingsModel.BucketSizes);
            TextureFileFormats = new ObservableCollection<string>(SettingsModel.TextureFileFormats);
            Axis = new ObservableCollection<string>(SettingsModel.Axis);
            Distributions = new ObservableCollection<string>(SettingsModel.Distributions);
            Algorithms = new ObservableCollection<string>(SettingsModel.Algorithms);
            CoordSystems = new ObservableCollection<string>(SettingsModel.CoordSystems);
            ToneMappings = new ObservableCollection<string>(SettingsModel.ToneMappings);
            Normalizations = new ObservableCollection<string>(SettingsModel.Normalizations);
            BrowseXnormal = new RelayCommand(BrowseXNExecutable);
            BrowseBakingPath = new RelayCommand(BrowseBakePath);
            CMDQuickBakes = new RelayCommand(SetQuickBakes);
            CMDResetALL = new RelayCommand(ResetAll);
            CheckXNPath();
            CheckBakePath();
            //SetQuickBakes();
        }

        private void RefreshData()
        {
            NotifyPropertyChanged("EdgePadding");
            NotifyPropertyChanged("BakingPath");
            NotifyPropertyChanged("BakeNormals");
            NotifyPropertyChanged("BakeAmbient");
            NotifyPropertyChanged("BakeHeight");
            NotifyPropertyChanged("BakeCurvature");
            NotifyPropertyChanged("BakeVertexColors");
            NotifyPropertyChanged("BakeWireframe");
            NotifyPropertyChanged("BakeTranslucency");
            NotifyPropertyChanged("BakeRadiosity");
            NotifyPropertyChanged("BakePRTpn");
            NotifyPropertyChanged("BakeProximity");
            NotifyPropertyChanged("BakeDirection");
            NotifyPropertyChanged("BakeDerivative");
            NotifyPropertyChanged("BakeConvexity");
            NotifyPropertyChanged("BakeCavity");
            NotifyPropertyChanged("BakeBentNormals");
            NotifyPropertyChanged("BakeBaseTexture");
            NotifyPropertyChanged("BakeThickness");
            NotifyPropertyChanged("SelectedAASize");
            NotifyPropertyChanged("SelectedBucketSize");
            NotifyPropertyChanged("SelectedTextureFileFormat");
            NotifyPropertyChanged("SettingsAmbient");
            NotifyPropertyChanged("SettingsBaseTexture");
            NotifyPropertyChanged("SettingsBentNormal");
            NotifyPropertyChanged("SettingsCavity");
            NotifyPropertyChanged("SettingsConvexity");
            NotifyPropertyChanged("SettingsCurvature");
            NotifyPropertyChanged("SettingsDerivative");
            NotifyPropertyChanged("SettingsDirection");
            NotifyPropertyChanged("SettingsHeight");
            NotifyPropertyChanged("SettingsNormal");
            NotifyPropertyChanged("SettingsProximity");
            NotifyPropertyChanged("SettingsPRTpn");
            NotifyPropertyChanged("SettingsRadiosity");
            NotifyPropertyChanged("SettingsTranslucency");
            NotifyPropertyChanged("SettingsVertexColors");
            NotifyPropertyChanged("SettingsWireframe");
        }

        private void ToggleAllPanels(bool show = true)
        {
            BakeAmbient = show;
            BakeBaseTexture = show;
            BakeBentNormals = show;
            BakeCavity = show;
            BakeConvexity = show;
            BakeCurvature = show;
            BakeDerivative = show;
            BakeDirection = show;
            BakeHeight = show;
            BakeNormals = show;
            BakeProximity = show;
            BakePRTpn = show;
            BakeRadiosity = show;
            //BakeThickness = show;
            BakeTranslucency = show;
            BakeVertexColors = show;
            BakeWireframe = show;
            NotifyPropertyChanged("BakeSettingsVisibility");
        }


        private void SetQuickBakes()
        {
            ToggleAllPanels(false);
            BakeNormals = true;
            BakeAmbient = true;
            BakeCurvature = true;
        }

        private void ResetAll()
        {

        }


        private void CheckXNPath()
        {
            if (!File.Exists(XNormalPath))
            {
                var uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                var HKLM64 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, Environment.MachineName, RegistryView.Registry64);
                RegistryKey key64 = HKLM64.OpenSubKey(uninstallKey);
                List<string> nameList = key64.GetSubKeyNames().ToList();

                string location = "";
                Regex xNormalRegEx = new Regex(@"xNormal ");
                foreach (string keyName in nameList.Distinct())
                {
                    RegistryKey subKey = key64.OpenSubKey(keyName);
                    if (subKey != null)
                    {
                        object key = subKey.GetValue("DisplayName");
                        if (key != null)
                        {
                            if (xNormalRegEx.IsMatch(key.ToString()))
                            {
                                location = $@"{subKey.GetValue("InstallLocation")}\x64\xNormal.exe";
                                if (File.Exists(location))
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                key64.Close();
                if (File.Exists(location))
                {
                    XNormalPath = location;
                }
                else
                {
                    DisplayUpdatePath();
                }
            }
        }
        private void DisplayUpdatePath()
        {
            string UpdatePath = "Please update the xNormal path in the setting Panel.\nYou can still generate the xml files for baking later.\nDo you want to continue?";
            MessageBoxResult result = System.Windows.MessageBox.Show(UpdatePath, "Error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.No)
            {
                //this.Close();
            }
        }
        private void CheckBakePath()
        {
            if (!Directory.Exists(BakingPath))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Baking Path doesn't exist.\n Please set it now.", "Error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                    BrowseBakePath();
            }
        }

        private void BrowseXNExecutable()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "xNormal", // Default file name 
                DefaultExt = ".exe", // Default file extension 
                Filter = "Executables (.exe)|*.exe" // Filter files by extension
            };
            try
            {
                dlg.InitialDirectory = Path.GetDirectoryName(XNormalPath);
            }
            catch
            {
                dlg.InitialDirectory = "";
            }
            if (dlg.ShowDialog() == true)
            {
                XNormalPath = dlg.FileName;
            }
        }

        private void BrowseBakePath()
        {
            var oPath = BakingPath;
            var dialog = new FolderBrowserDialog
            {
                Description = "Open a folder which will contains the baking directories:",
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = BakingPath
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                BakingPath = dialog.SelectedPath + @"\";
            }
            Helpers.FileHelper.CreateSubFolders(BakingPath);
            if (oPath != BakingPath)
            {
                BatchViewModel.Instance.RefreshBatchItems();
            }
        }
    }
}
