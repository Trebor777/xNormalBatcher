using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using XnormalBatcher.Helpers;
using XnormalBatcher.ViewModels;

namespace XnormalBatcher.ViewModels
{
    // Holds information/logic on how to batch, which files to batch, etc...
    internal class BatchViewModel : BaseViewModel
    {
        public static BatchViewModel Instance { get; } = new BatchViewModel();
        public ObservableCollection<int> MapSizes { get; set; }
        public ObservableCollection<string> MeshFileFormats { get; set; }
        public ObservableCollection<BatchItemViewModel> BatchItems { get; set; } = new ObservableCollection<BatchItemViewModel>();
        public BatchItemViewModel SelectedBatchItem { get; set; }
        private BatchItemViewModel GlobalBatchItem { get; set; }
        public bool UseCage { get => useCage; set { useCage = value; RefreshBatchItems(); } }
        public bool UseTermsAsPrefix { get => useTermsAsPrefix; set { useTermsAsPrefix = value; RefreshBatchItems(); } }
        public string SelectedTermSeparator { get => selectedTermSeparator; set { selectedTermSeparator = value; RefreshBatchItems(); } }
        public string SelectedTermLow { get => selectedTermLow; set { selectedTermLow = value; RefreshBatchItems(); } }
        public string SelectedTermHigh { get => selectedTermHigh; set { selectedTermHigh = value; RefreshBatchItems(); } }
        public string SelectedTermCage { get => selectedTermCage; set { selectedTermCage = value; RefreshBatchItems(); } }

        public string[] SelectedTerms => new string[] { SelectedTermLow, SelectedTermHigh, SelectedTermCage };
        public string[] SelectedFormats => new string[] { SelectedMeshFormatLow, SelectedMeshFormatHigh, SelectedMeshFormatCage, SettingsViewModel.Instance.SelectedTextureFileFormat };

        public string SelectedMeshFormatLow { get => selectedMeshFormatLow; set { selectedMeshFormatLow = value; RefreshBatchItems(); } }
        public string SelectedMeshFormatHigh { get => selectedMeshFormatHigh; set { selectedMeshFormatHigh = value; RefreshBatchItems(); } }
        public string SelectedMeshFormatCage { get => selectedMeshFormatCage; set { selectedMeshFormatCage = value; RefreshBatchItems(); } }
        public bool BakeSeparately { get; set; }
        public int SelectedMapWidthAll { get; set; }
        public int SelectedMapHeightAll { get; set; }
        public int FileLowCount { get => _fileLCount; set { _fileLCount = value; NotifyPropertyChanged(); } }
        public int FileHighCount { get => _fileHCount; set { _fileHCount = value; NotifyPropertyChanged(); } }
        public int FileCageCount { get => _fileCCount; set { _fileCCount = value; NotifyPropertyChanged(); } }

        public ICommand CMDOpenFolder { get; set; }
        public ICommand CMDBakeSelected { get; set; }
        public ICommand CMDBakeAll { get; set; }

        public ICommand CMDSetAllItemsLow { get; set; }
        public ICommand CMDSetAllItemsHigh { get; set; }
        public ICommand CMDSetAllMaps { get; set; }

        private RelayParametrizedCommand checkAllCommand;

        public ICommand CheckAllCommand
        {
            get
            {
                return checkAllCommand ?? (checkAllCommand = new RelayParametrizedCommand(param => CheckAll(bool.Parse(param.ToString()))));
            }
        }

        private void CheckAll(bool selectAll)
        {
            foreach (var item in BatchItems)
            {
                item.IsSelected = selectAll;
            }
        }

        public FileSystemWatcher AutoUpdater = new FileSystemWatcher();
        private int _fileLCount;
        private int _fileHCount;
        private int _fileCCount;
        private bool useCage;
        private string selectedTermLow;
        private bool useTermsAsPrefix;
        private string selectedTermHigh;
        private string selectedTermCage;
        private string selectedMeshFormatLow;
        private string selectedMeshFormatHigh;
        private string selectedMeshFormatCage;
        private string selectedTermSeparator;

        private BatchViewModel()
        {
            
            UseCage = false;
            UseTermsAsPrefix = false;
            BakeSeparately = false;
            MapSizes = new ObservableCollection<int>() { 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 };
            MeshFileFormats = new ObservableCollection<string>() { "fbx", "sia", "sib", "x", "ms3d", "off", "dae", "ovb", "dxf", "mesh", "xsi", "3ds", "sbm", "obj", "ase", "ply", "lwo", "lxo" };
            //Default Selection
            SelectedMapWidthAll = SelectedMapHeightAll = MapSizes[7];
            SelectedMeshFormatLow = SelectedMeshFormatHigh = SelectedMeshFormatCage = MeshFileFormats[13];

            FileLowCount = FileHighCount = FileCageCount = 0;

            CMDOpenFolder = new RelayCommand(OpenFolder);
            CMDBakeSelected = new RelayParametrizedCommand(a => Bake());
            CMDBakeAll = new RelayParametrizedCommand(a => Bake(true));

            CMDSetAllMaps = new RelayCommand(SetAllMaps);
            CMDSetAllItemsLow = new RelayCommand(SetAllLow);
            CMDSetAllItemsHigh = new RelayCommand(SetAllHigh);

            SelectedTermSeparator = TermsViewModel.Instance.TermsSeparator[0];
            SelectedTermLow = TermsViewModel.Instance.TermsLow[0];
            SelectedTermHigh = TermsViewModel.Instance.TermsHigh[0];
            SelectedTermCage = TermsViewModel.Instance.TermsCage[0];


            AutoUpdater.IncludeSubdirectories = true;
            AutoUpdater.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            AutoUpdater.Changed += new FileSystemEventHandler(OnRefreshFiles);
            AutoUpdater.Created += new FileSystemEventHandler(OnRefreshFiles);
            AutoUpdater.Deleted += new FileSystemEventHandler(OnRefreshFiles);
            AutoUpdater.Renamed += new RenamedEventHandler(OnRefreshFiles);
            AutoUpdater.Path = SettingsViewModel.Instance.BakingPath;
            AutoUpdater.EnableRaisingEvents = SettingsViewModel.Instance.BakingPath != null;

            GlobalBatchItem = new BatchItemViewModel("__global__", this);
        }

        public string GetSuffix(int slot)
        {
            var term = SelectedTerms[slot];
            return FileHelper.CreateSuffix(term, SelectedTermSeparator, UseTermsAsPrefix);
        }

        private void OpenFolder()
        {
            if (Directory.Exists(SettingsViewModel.Instance.BakingPath))
            {
                _ = Process.Start(SettingsViewModel.Instance.BakingPath);
            }
        }

        private void Bake(bool all = false)
        {
            var results = BatchItems.Where(b => (b.IsSelected || all) && b.IsValid).Select(b => b.Bake(true).Result != 0 ? b.Name : null).Select(n => n != null);
            if (results.Count() > 0)
            {
                MessageBox.Show($"These asset(s) couldn't be baked:\n{string.Join("\n", results)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Look for files in baking folders, according to choosen naming convention (suffix, extension etc...) 
        /// </summary>
        /// <returns></returns>
        private string[] GetRefFolderFiles()
        {
            var path = SettingsViewModel.Instance.BakingPath;
            string[] files_L = FileHelper.GetItems(path + FileHelper.SubFolders[0], SelectedMeshFormatLow, GetSuffix(0), UseTermsAsPrefix);
            string[] files_H = FileHelper.GetItems(path + FileHelper.SubFolders[1], SelectedMeshFormatHigh, GetSuffix(1), UseTermsAsPrefix);
            string[] files_C = FileHelper.GetItems(path + FileHelper.SubFolders[2], SelectedMeshFormatCage, GetSuffix(2), UseTermsAsPrefix);
            List<string[]> mains = new List<string[]>() { files_L, files_H, files_C };
            FileLowCount = files_L.Length;
            FileHighCount = files_H.Length;
            FileCageCount = UseCage ? files_C.Length : 0;
            return mains.Aggregate((i1, i2) => i1.Length > i2.Length ? i1 : i2);
        }
        /// <summary>
        /// Regenerates the list of bake objects
        /// </summary>
        internal void RefreshBatchItems()
        {
            if (MainViewModel.Instance.IsLoaded)
            {
                BatchItems.Clear();
                foreach (string item in GetRefFolderFiles())
                {
                    var data = new BatchItemViewModel(item);
                    BatchItems.Add(data);
                }
            }

        }

        public void OnRefreshFiles(object sender, EventArgs e)
        {
            var dispatcher = Application.Current.Dispatcher;
            if (!dispatcher.CheckAccess())
            {
                dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    (FileSystemEventHandler)OnRefreshFiles, sender, e);
            }
            else
            {
                if (MainViewModel.Instance.IsLoaded)
                {
                    RefreshBatchItems();
                }
            }
        }

        private void SetAllMaps()
        {
            foreach (var item in BatchItems)
            {
                item.Width = SelectedMapWidthAll;
                item.Height = SelectedMapHeightAll;
            }
        }

        private void ApplyGlobalSettings(bool low = true)
        {
            foreach (var item in BatchItems)
            {
                if (low)
                {
                    item.SettingsLow = new MeshSettingsLowVM(GlobalBatchItem.SettingsLow);
                }
                else
                {
                    item.SettingsHigh = new MeshSettingsHighVM(GlobalBatchItem.SettingsHigh);
                }
                item.GenerateXml();
            }
        }


        private void SetAllLow()
        {
            Window dlg = new WindowLowPoly(GlobalBatchItem.SettingsLow)
            {
                Owner = Application.Current.MainWindow
            };
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                ApplyGlobalSettings(true);
            }
        }

        private void SetAllHigh()
        {
            Window dlg = new WindowHighPoly(GlobalBatchItem.SettingsHigh)
            {
                Owner = Application.Current.MainWindow
            };
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                ApplyGlobalSettings(false);
            }
        }

    }
}
