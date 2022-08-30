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
    internal class SettingsViewModel : BaseViewModel
    {
        public static SettingsViewModel Instance { get; } = new SettingsViewModel();

        private string _xnPath;
        private string _bakePath;
        private ObservableCollection<int> aaSizes;
        private ObservableCollection<int> bucketSizes;
        private ObservableCollection<string> textureFileFormats;
        private ObservableCollection<string> axis;
        private ObservableCollection<string> distributions;
        private ObservableCollection<string> algorithms;
        private ObservableCollection<string> coordSystems;
        private ObservableCollection<string> toneMappings;
        private int selectedAASize;
        private int selectedBucketSize;
        private string selectedTextureFileFormat;
        private bool bakeNormals;
        private bool bothNormalsType;
        private bool bakeAmbient;
        private bool bakeHeight;
        private bool bakeCurvature;
        private bool bakeVertexColors;
        private bool bakeThickness;
        private bool bakeCavity;
        private bool bakeBentNormals;
        private bool bakeDirection;
        private bool bakeConvexity;
        private bool bakeDerivative;
        private bool bakeRadiosity;
        private bool bakeProximity;
        private bool bakePRTpn;
        private bool bakeBaseTexture;
        private bool bakeWireframe;
        private bool bakeTranslucency;
        private bool closestHitRayFails;
        private bool discardBackFaceHit;
        private VMSettingsAmbient settingsAmbient;
        private VMSettingsBaseTexture settingsBaseTexture;
        private VMSettingsBentNormal settingsBentNormal;
        private VMSettingsCavity settingsCavity;
        private VMSettingsConvexity settingsConvexity;
        private VMSettingsCurvature settingsCurvature;
        private VMSettingsDerivative settingsDerivative;
        private VMSettingsDirection settingsDirection;
        private VMSettingsHeight settingsHeight;
        private VMSettingsNormal settingsNormal;
        private VMSettingsProximity settingsProximity;
        private VMSettingsPRTpn settingsPRTpn;
        private VMSettingsRadiosity settingsRadiosity;
        private VMSettingsTranslucency settingsTranslucency;
        private VMSettingsVertexColors settingsVertexColors;
        private VMSettingsWireframe settingsWireframe;
        private int edgePadding;
        private ObservableCollection<string> normalizations;

        //private VMSettingsThickness settingsThickness;

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
            get => edgePadding; set
            {
                edgePadding = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public string XNormalPath
        {
            get => _xnPath; set
            {
                _xnPath = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public string BakingPath
        {
            get => _bakePath; set
            {
                _bakePath = value;
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
            get => selectedAASize; set
            {
                selectedAASize = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public int SelectedBucketSize
        {
            get => selectedBucketSize; set
            {
                selectedBucketSize = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public string SelectedTextureFileFormat
        {
            get => selectedTextureFileFormat; set
            {
                selectedTextureFileFormat = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeNormals
        {
            get => bakeNormals; set
            {
                bakeNormals = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BothNormalsType
        {
            get => bothNormalsType; set
            {
                bothNormalsType = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeAmbient
        {
            get => bakeAmbient; set
            {
                bakeAmbient = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeHeight
        {
            get => bakeHeight; set
            {
                bakeHeight = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeCurvature
        {
            get => bakeCurvature; set
            {
                bakeCurvature = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeVertexColors
        {
            get => bakeVertexColors; set
            {
                bakeVertexColors = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeThickness
        {
            get => bakeThickness; set
            {
                bakeThickness = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeCavity
        {
            get => bakeCavity; set
            {
                bakeCavity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeBentNormals
        {
            get => bakeBentNormals; set
            {
                bakeBentNormals = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeDirection
        {
            get => bakeDirection; set
            {
                bakeDirection = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeConvexity
        {
            get => bakeConvexity; set
            {
                bakeConvexity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeDerivative
        {
            get => bakeDerivative; set
            {
                bakeDerivative = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeRadiosity
        {
            get => bakeRadiosity; set
            {
                bakeRadiosity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeProximity
        {
            get => bakeProximity; set
            {
                bakeProximity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakePRTpn
        {
            get => bakePRTpn; set
            {
                bakePRTpn = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeBaseTexture
        {
            get => bakeBaseTexture; set
            {
                bakeBaseTexture = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeWireframe
        {
            get => bakeWireframe; set
            {
                bakeWireframe = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool BakeTranslucency
        {
            get => bakeTranslucency; set
            {
                bakeTranslucency = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool ClosestHitRayFails
        {
            get => closestHitRayFails; set
            {
                closestHitRayFails = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public bool DiscardBackFaceHit
        {
            get => discardBackFaceHit; set
            {
                discardBackFaceHit = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsAmbient SettingsAmbient
        {
            get => settingsAmbient; set
            {
                settingsAmbient = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsBaseTexture SettingsBaseTexture
        {
            get => settingsBaseTexture; set
            {
                settingsBaseTexture = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsBentNormal SettingsBentNormal
        {
            get => settingsBentNormal; set
            {
                settingsBentNormal = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsCavity SettingsCavity
        {
            get => settingsCavity; set
            {
                settingsCavity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsConvexity SettingsConvexity
        {
            get => settingsConvexity; set
            {
                settingsConvexity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsCurvature SettingsCurvature
        {
            get => settingsCurvature; set
            {
                settingsCurvature = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsDerivative SettingsDerivative
        {
            get => settingsDerivative; set
            {
                settingsDerivative = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsDirection SettingsDirection
        {
            get => settingsDirection; set
            {
                settingsDirection = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsHeight SettingsHeight
        {
            get => settingsHeight; set
            {
                settingsHeight = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsNormal SettingsNormal
        {
            get => settingsNormal; set
            {
                settingsNormal = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsProximity SettingsProximity
        {
            get => settingsProximity; set
            {
                settingsProximity = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsPRTpn SettingsPRTpn
        {
            get => settingsPRTpn; set
            {
                settingsPRTpn = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsRadiosity SettingsRadiosity
        {
            get => settingsRadiosity; set
            {
                settingsRadiosity = value;
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
            get => settingsTranslucency; set
            {
                settingsTranslucency = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsVertexColors SettingsVertexColors
        {
            get => settingsVertexColors; set
            {
                settingsVertexColors = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty]
        public VMSettingsWireframe SettingsWireframe
        {
            get => settingsWireframe; set
            {
                settingsWireframe = value;
                NotifyPropertyChanged();
            }
        }
        private SettingsViewModel()
        {
            BakingPath = @"E:\bake\";
            EdgePadding = 16;
            AASizes = new ObservableCollection<int>() { 1, 2, 4 };
            BucketSizes = new ObservableCollection<int>() { 16, 32, 64, 128, 256, 512 };
            TextureFileFormats = new ObservableCollection<string>() { "tga", "tif", "tiff", "jpg", "png", "raw", "dds", "j2k", "wdp", "jxr", "hdp", "hdr", "imgj", "webp", "exr" };
            Axis = new ObservableCollection<string>() { "X+", "X-", "Y+", "Y-", "Z+", "Z-" };
            Distributions = new ObservableCollection<string>() { "Uniform", "Cosine", "CosineSq" };
            Algorithms = new ObservableCollection<string>() { "Average", "Gaussian" };
            CoordSystems = new ObservableCollection<string>() { "OpenGL", "DirectX", "AliB" };
            ToneMappings = new ObservableCollection<string>() { "Monocrome", "2Col", "3Col" };
            Normalizations = new ObservableCollection<string>() { "Interactive", "Manual", "Raw FP Values" };
            BrowseXnormal = new RelayCommand(BrowseXNExecutable);
            BrowseBakingPath = new RelayCommand(BrowseBakePath);
            CMDQuickBakes = new RelayCommand(SetQuickBakes);
            CMDResetALL = new RelayCommand(ResetAll);
            CheckXNPath();
            CheckBakePath();
            SelectedAASize = AASizes[0];
            SelectedBucketSize = BucketSizes[2];
            SelectedTextureFileFormat = TextureFileFormats[4];
            CreateSettings();
            SetQuickBakes();
            //ToggleAllPanels(true);
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

        private void CreateSettings()
        {
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
            //SettingsThickness = new VMSettingsThickness();
            SettingsTranslucency = new VMSettingsTranslucency();
            SettingsVertexColors = new VMSettingsVertexColors();
            SettingsWireframe = new VMSettingsWireframe();
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
