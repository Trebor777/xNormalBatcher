using Microsoft.Win32;
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
    internal class SettingsViewModel : BaseViewModel
    {
        public static SettingsViewModel Instance { get; } = new SettingsViewModel();
        public int EdgePadding { get; set; }
        public bool BakeSettingsVisibility // Show the Settings Panel depending if any map to bake is checked. Else Hide it. Uses a BooleanToVisibilityConverter
        {
            get
            {
                return BakeNormals || BakeAmbient || BakeHeight || BakeCurvature || BakeVertexColors
                    || BakeWireframe || BakeTranslucency || BakeThickness || BakeRadiosity
                    || BakePRTpn || BakeProximity || BakeDirection || BakeDerivative || BakeConvexity
                    || BakeCavity || BakeBentNormals || BakeBaseTexture;
            }

            set { }
        }
        public List<string> SubFolders { get; set; }
        public ICommand BrowseXnormal { get; set; }
        public ICommand BrowseBakingPath { get; set; }
        private string _xnPath;
        private string _bakePath;
        public string XNormalPath { get => _xnPath; set { _xnPath = value; NotifyPropertyChanged(); } }
        public string BakingPath { get => _bakePath; set { _bakePath = value; NotifyPropertyChanged(); } }
        public ObservableCollection<int> AASizes { get; set; }
        public ObservableCollection<int> BucketSizes { get; set; }
        public ObservableCollection<string> TextureFileFormats { get; set; }
        public ObservableCollection<string> Axis { get; set; }
        public ObservableCollection<string> Distributions { get; set; }
        public ObservableCollection<string> Algorithms { get; set; }
        public ObservableCollection<string> CoordSystems { get; set; }
        public ObservableCollection<string> ToneMappings { get; set; }
        public int SelectedAASize { get; set; }
        public int SelectedBucketSize { get; set; }
        public string SelectedTextureFileFormat { get; set; }
        /// <summary>
        /// Selected Bake Options Properties
        /// </summary>
        public bool BakeNormals { get; set; }
        public bool BothNormalsType { get; set; }
        public bool BakeAmbient { get; set; }
        public bool BakeHeight { get; set; }
        public bool BakeCurvature { get; set; }
        public bool BakeVertexColors { get; set; }
        public bool BakeThickness { get; set; }
        public bool BakeCavity { get; set; }
        public bool BakeBentNormals { get; set; }
        public bool BakeDirection { get; set; }
        public bool BakeConvexity { get; set; }
        public bool BakeDerivative { get; set; }
        public bool BakeRadiosity { get; set; }
        public bool BakeProximity { get; set; }
        public bool BakePRTpn { get; set; }
        public bool BakeBaseTexture { get; set; }
        public bool BakeWireframe { get; set; }
        public bool BakeTranslucency { get; set; }

        public VMSettingsAmbient SettingsAmbient { get; set; }
        public VMSettingsBaseTexture SettingsBaseTexture { get; set; }
        public VMSettingsBentNormal SettingsBentNormal { get; set; }
        public VMSettingsCavity SettingsCavity { get; set; }
        public VMSettingsConvexity SettingsConvexity { get; set; }
        public VMSettingsCurvature SettingsCurvature { get; set; }
        public VMSettingsDerivative SettingsDerivative { get; set; }
        public VMSettingsDirection SettingsDirection { get; set; }
        public VMSettingsHeight SettingsHeight { get; set; }
        public VMSettingsNormal SettingsNormal { get; set; }
        public VMSettingsProximity SettingsProximity { get; set; }
        public VMSettingsPRTpn SettingsPRTpn { get; set; }
        public VMSettingsRadiosity SettingsRadiosity { get; set; }
        public VMSettingsTranslucency SettingsTranslucency { get; set; }
        public VMSettingsVertexColors SettingsVertexColors { get; set; }
        public VMSettingsWireframe SettingsWireframe { get; set; }
        private SettingsViewModel()
        {
            BakingPath = @"E:\bake\";
            EdgePadding = 16;
            BakeSettingsVisibility = false;
            AASizes = new ObservableCollection<int>() { 1, 2, 4 };
            BucketSizes = new ObservableCollection<int>() { 16, 32, 64, 128, 256, 512 };
            TextureFileFormats = new ObservableCollection<string>() { "tga", "tif", "tiff", "jpg", "png", "raw", "dds", "j2k", "wdp", "jxr", "hdp", "hdr", "imgj", "webp", "exr" };
            Axis = new ObservableCollection<string>() { "X+", "X-", "Y+", "Y-", "Z+", "Z-" };
            Distributions = new ObservableCollection<string>() { "Uniform", "Cosine", "CosineSq" };
            Algorithms = new ObservableCollection<string>() { "Average", "Gaussian" };
            CoordSystems = new ObservableCollection<string>() { "OpenGL", "DirectX", "AliB" };
            ToneMappings = new ObservableCollection<string>() { "Monocrome", "2Col", "3Col" };
            SubFolders = new List<string>() { @"LowPoly\", @"HighPoly\", @"Cage\", @"Maps\" };
            BrowseXnormal = new RelayCommand(BrowseXNExecutable);
            BrowseBakingPath = new RelayCommand(BrowseBakePath);
            CheckXNPath();
            CheckBakePath();
            SelectedAASize = AASizes[0];
            SelectedBucketSize = BucketSizes[2];
            SelectedTextureFileFormat = TextureFileFormats[4];
            BakeNormals = true;
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
                if (result == MessageBoxResult.No)
                {
                    //this.Close();
                }
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
                //BakeData.Clear();
            }
        }
    }
}
