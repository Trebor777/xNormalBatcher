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
        public string XNormalPath { get; set; }
        public string BakingPath { get; set; }
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

        public SettingsViewModel()
        {
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
                try
                {
                    RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    List<string> nameList = regKey.GetSubKeyNames().ToList();
                    string location = "";
                    Regex xNormalRegEx = new Regex(@"xNormal \d*");
                    foreach (string keyName in nameList)
                    {
                        RegistryKey subKey = regKey.OpenSubKey(keyName);
                        try
                        {
                            var key = subKey.GetValue("DisplayName");
                            if (key != null)
                            {
                                if (xNormalRegEx.IsMatch(key.ToString()))
                                {
                                    location = subKey.GetValue("InstallLocation").ToString() + @"\x64\xNormal.exe";
                                    if (File.Exists(location))
                                        break;
                                }
                            }
                        }
                        catch { }
                    }
                    regKey.Close();
                    if (File.Exists(location))
                        XNormalPath = location;
                    else
                        DisplayUpdatePath();
                }
                catch
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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "xNormal"; // Default file name 
            dlg.DefaultExt = ".exe"; // Default file extension 
            dlg.Filter = "Executables (.exe)|*.exe"; // Filter files by extension
            try
            {
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(XNormalPath);
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
                BakingPath = dialog.SelectedPath + "\\";
            }
            Helpers.FileHelper.CreateSubFolders(BakingPath, SubFolders);
            if (oPath != BakingPath)
            {
                //BakeData.Clear();
            }
        }
    }
}
