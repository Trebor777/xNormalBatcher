using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    [JsonObject(MemberSerialization.OptOut)]
    internal class BatchModel
    {
        [JsonIgnore]
        internal static List<int> MapSizes = new List<int>() { 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 };
        [JsonIgnore]
        internal static List<string> MeshFileFormats = new List<string>() { "fbx", "sia", "sib", "x", "ms3d", "off", "dae", "ovb", "dxf", "mesh", "xsi", "3ds", "sbm", "obj", "ase", "ply", "lwo", "lxo" };
        public bool UseCage { get; set; }
        public bool UseTermsAsPrefix { get; set; }
        public Term SelectedTermSeparator { get; set; }
        public Term SelectedTermLow { get; set; }
        public Term SelectedTermHigh { get; set; }
        public Term SelectedTermCage { get; set; }
        public string SelectedMeshFormatLow { get; set; }
        public string SelectedMeshFormatHigh { get; set; }
        public string SelectedMeshFormatCage { get; set; }
        public bool BakeSeparately { get; set; }
        public int SelectedMapWidthAll { get; set; }
        public int SelectedMapHeightAll { get; set; }


        [JsonConstructor]
        private BatchModel()
        { }

        public BatchModel(int n = 0)
        {
            SelectedTermSeparator = TermsModel.TermsSeparator[0];
            SelectedTermLow = TermsModel.TermsLow[0];
            SelectedTermHigh = TermsModel.TermsHigh[0];
            SelectedTermCage = TermsModel.TermsCage[0];
            SelectedMeshFormatLow = MeshFileFormats[13];
            SelectedMeshFormatHigh = MeshFileFormats[13];
            SelectedMeshFormatCage = MeshFileFormats[13];
            BakeSeparately = false;
            UseCage = false;
            UseTermsAsPrefix = false;
            SelectedMapWidthAll = MapSizes[7];
            SelectedMapHeightAll = MapSizes[7];
        }
    }

    internal class BatchViewModel : BaseViewModel
    {
        internal BatchModel Data;
        private RelayParametrizedCommand checkAllCommand;
        public FileSystemWatcher AutoUpdater = new FileSystemWatcher();
        private int _fileLCount;
        private int _fileHCount;
        private int _fileCCount;
        private bool isBaking;
        private double bakingProgress;
        private ObservableCollection<string> logEntries;
        public static BatchViewModel Instance { get; } = new BatchViewModel();
        public ObservableCollection<int> MapSizes { get; set; } = new ObservableCollection<int>(BatchModel.MapSizes);
        public ObservableCollection<string> MeshFileFormats { get; set; } = new ObservableCollection<string>(BatchModel.MeshFileFormats);
        public ObservableCollection<BatchItemViewModel> BatchItems { get; set; } = new ObservableCollection<BatchItemViewModel>();
        public BatchItemViewModel SelectedBatchItem { get; set; }
        private BatchItemViewModel GlobalBatchItem { get; set; }

        public bool UseCage { get => Data.UseCage; set { Data.UseCage = value; RefreshBatchItems(); } }
        public bool UseTermsAsPrefix { get => Data.UseTermsAsPrefix; set { Data.UseTermsAsPrefix = value; RefreshBatchItems(); } }
        public Term SelectedTermSeparator { get => Data.SelectedTermSeparator; set { Data.SelectedTermSeparator = value; RefreshBatchItems(); } }
        public Term SelectedTermLow { get => Data.SelectedTermLow; set { Data.SelectedTermLow = value; RefreshBatchItems(); } }
        public Term SelectedTermHigh { get => Data.SelectedTermHigh; set { Data.SelectedTermHigh = value; RefreshBatchItems(); } }
        public Term SelectedTermCage { get => Data.SelectedTermCage; set { Data.SelectedTermCage = value; RefreshBatchItems(); } }
        public string[] SelectedTerms => new string[] { SelectedTermLow?.Name, SelectedTermHigh?.Name, SelectedTermCage?.Name };
        public string[] SelectedFormats => new string[] { SelectedMeshFormatLow, SelectedMeshFormatHigh, SelectedMeshFormatCage, SettingsViewModel.Instance.SelectedTextureFileFormat };
        public string SelectedMeshFormatLow { get => Data.SelectedMeshFormatLow; set { Data.SelectedMeshFormatLow = value; RefreshBatchItems(); } }
        public string SelectedMeshFormatHigh { get => Data.SelectedMeshFormatHigh; set { Data.SelectedMeshFormatHigh = value; RefreshBatchItems(); } }
        public string SelectedMeshFormatCage { get => Data.SelectedMeshFormatCage; set { Data.SelectedMeshFormatCage = value; RefreshBatchItems(); } }
        public bool BakeSeparately { get => Data.BakeSeparately; set => Data.BakeSeparately = value; }
        public int SelectedMapWidthAll { get => Data.SelectedMapWidthAll; set => Data.SelectedMapWidthAll = value; }
        public int SelectedMapHeightAll { get => Data.SelectedMapHeightAll; set => Data.SelectedMapHeightAll = value; }
        public int FileLowCount { get => _fileLCount; set { _fileLCount = value; NotifyPropertyChanged(); } }
        public int FileHighCount { get => _fileHCount; set { _fileHCount = value; NotifyPropertyChanged(); } }
        public int FileCageCount { get => _fileCCount; set { _fileCCount = value; NotifyPropertyChanged(); } }
        public bool IsBaking
        {
            get => isBaking;
            set { isBaking = value; NotifyPropertyChanged(); }
        }
        public double BakingProgress
        {
            get => bakingProgress;
            set { bakingProgress = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<string> LogEntries
        {
            get => logEntries;
            set { logEntries = value; NotifyPropertyChanged(); }
        }
        public string LastLogEntry => LogEntries.Last();
        public ICommand CMDOpenFolder { get; set; }
        public ICommand CMDOpenLog { get; set; }
        public ICommand CMDBakeSelected { get; set; }
        public ICommand CMDBakeAll { get; set; }
        public ICommand CMDSetAllItemsLow { get; set; }
        public ICommand CMDSetAllItemsHigh { get; set; }
        public ICommand CMDSetAllMaps { get; set; }
        public ICommand CheckAllCommand => checkAllCommand ?? (checkAllCommand = new RelayParametrizedCommand(param => CheckAll(bool.Parse(param.ToString()))));
        private BatchViewModel()
        {
            LogEntries = new ObservableCollection<string>();
            Log("XNormalBatcher Started.");
            IsBaking = false;
            //Default Selection
            FileLowCount = FileHighCount = FileCageCount = 0;
            Data = MainViewModel.LastSession?.Batch ?? new BatchModel(0);
            RefreshData();
            // Commands
            CMDOpenFolder = new RelayCommand(OpenFolder);
            CMDOpenLog = new RelayCommand(OpenLog);
            CMDBakeSelected = new RelayParametrizedCommand(a => Bake());
            CMDBakeAll = new RelayParametrizedCommand(a => Bake(true));
            CMDSetAllMaps = new RelayCommand(SetAllMaps);
            CMDSetAllItemsLow = new RelayCommand(SetAllLow);
            CMDSetAllItemsHigh = new RelayCommand(SetAllHigh);            
            
        }

        private void RefreshData()
        {
            // Reference fix for terms after deserialisation
            Data.SelectedTermCage = TermsViewModel.Instance.Terms.FirstOrDefault(t => t.Name == Data.SelectedTermCage.Name && t.Group == Data.SelectedTermCage.Group);
            Data.SelectedTermLow = TermsViewModel.Instance.Terms.FirstOrDefault(t => t.Name == Data.SelectedTermLow.Name && t.Group == Data.SelectedTermLow.Group);
            Data.SelectedTermHigh = TermsViewModel.Instance.Terms.FirstOrDefault(t => t.Name == Data.SelectedTermHigh.Name && t.Group == Data.SelectedTermHigh.Group);
            Data.SelectedTermSeparator = TermsViewModel.Instance.Terms.FirstOrDefault(t => t.Name == Data.SelectedTermSeparator.Name && t.Group == Data.SelectedTermSeparator.Group);
            //
            NotifyPropertyChanged("UseCage");
            NotifyPropertyChanged("UseTermsAsPrefix");
            NotifyPropertyChanged("SelectedTermSeparator");
            NotifyPropertyChanged("SelectedTermLow");
            NotifyPropertyChanged("SelectedTermHigh");
            NotifyPropertyChanged("SelectedTermCage");
            NotifyPropertyChanged("SelectedMeshFormatLow");
            NotifyPropertyChanged("SelectedMeshFormatHigh");
            NotifyPropertyChanged("SelectedMeshFormatCage");
            NotifyPropertyChanged("BakeSeparately");
            NotifyPropertyChanged("SelectedMapWidthAll");
            NotifyPropertyChanged("SelectedMapHeightAll");
        }
        private void OpenLog()
        {
            new LogWindow().Show();
        }
        public void Log(string line)
        {
            LogEntries.Add(line);
            NotifyPropertyChanged("LastLogEntry");
        }
        private void CheckAll(bool selectAll)
        {
            foreach (var item in BatchItems)
            {
                item.IsSelected = selectAll;
            }
        }
        public string GetMeshSuffix(FileHelper.Slot slot)
        {
            var term = SelectedTerms[(int)slot];
            return FileHelper.CreateMeshSuffix(term, SelectedTermSeparator?.Name, UseTermsAsPrefix);
        }

        private void OpenFolder()
        {
            if (Directory.Exists(SettingsViewModel.Instance.BakingPath))
            {
                _ = Process.Start(SettingsViewModel.Instance.BakingPath);
            }
        }

        private void BakeWorker(object sender, DoWorkEventArgs e)
        {
            var items = BatchItems.Where(b => (b.IsSelected || (bool)e.Argument) && b.IsValid).ToArray();
            if (items.Length == 0)
                Log($"WARNING: No valid items to bake...");
            else
            {
                Log($"INFO: Processing {items.Length} items...");
            }
            for (int i = 0; i < items.Length; i++)
            {
                int result = items[i].BakeWorker();
                double percentage = (double)(i + 1) / items.Length * 100;
                (BatchItemViewModel Item, int Result) data = (Item: items[i], Result: result);
                (sender as BackgroundWorker).ReportProgress((int)percentage, data);
            }
        }

        private void Bake(bool all = false)
        {
            BakingProgress = 0;
            IsBaking = true;
            BackgroundWorker worker = new()
            {
                WorkerReportsProgress = true
            };
            worker.DoWork += BakeWorker;
            worker.ProgressChanged += BakeProgressChanged;
            worker.RunWorkerCompleted += BakeCompleted;
            worker.RunWorkerAsync(all);
        }

        private void BakeCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBaking = false;
            Log($"Processing done!");
        }

        private void BakeProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BakingProgress = e.ProgressPercentage;
            (BatchItemViewModel Item, int Result) = ((BatchItemViewModel Item, int Result))e.UserState;
            if (Result != 0)
            {
                if (Result == -1)
                {
                    Log($"ERROR: Asset can't be baked as it's invalid: {Item.Name}");
                }
                else
                {
                    Log($"ERROR: User aborted or An error has occured{ (!UseCage ? "" : " (probably Cage different from lowpoly mesh)")} with asset: {Item.Name}");
                }
            }
            else
            {
                Log($"{Item.Name}'s maps have been baked successfully!");
                Item.Validate();
                Item.IsSelected = false;
            }
        }

        /// <summary>
        /// Look for files in baking folders, according to choosen naming convention (suffix, extension etc...) 
        /// </summary>
        /// <returns></returns>
        private string[] GetRefFolderFiles()
        {
            var path = SettingsViewModel.Instance.BakingPath;
            string[] files_L = FileHelper.GetItems(path + FileHelper.SubFolders[(int)FileHelper.Slot.LP],   SelectedMeshFormatLow,  GetMeshSuffix(FileHelper.Slot.LP),      UseTermsAsPrefix);
            string[] files_H = FileHelper.GetItems(path + FileHelper.SubFolders[(int)FileHelper.Slot.HP],   SelectedMeshFormatHigh, GetMeshSuffix(FileHelper.Slot.HP),      UseTermsAsPrefix);
            string[] files_C = FileHelper.GetItems(path + FileHelper.SubFolders[(int)FileHelper.Slot.Cage], SelectedMeshFormatCage, GetMeshSuffix(FileHelper.Slot.Cage),    UseTermsAsPrefix);
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
            if (!MainViewModel.Instance.Initialized || AutoUpdater.Path == null)
            {
                return;
            }
            BatchItems.Clear();
            foreach (string item in GetRefFolderFiles())
            {
                var data = new BatchItemViewModel(item);
                BatchItems.Add(data);
            }

        }

        public void OnRefreshFiles(object sender, FileSystemEventArgs e)
        {
            // Ignore XML File changes.
            if (!MainViewModel.Instance.IsLoaded || e.FullPath.ToLower().EndsWith(".xml"))
            {
                return;
            }

            var dispatcher = Application.Current.Dispatcher;
            if (!dispatcher.CheckAccess())
            {
                dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    (FileSystemEventHandler)OnRefreshFiles, sender, e);
            }
            else
            {                
                RefreshBatchItems();
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

        internal void InitializeData(string bakingPath)
        {
            if (bakingPath != null && MainViewModel.Instance.Initialized)
            {
                // Data
                GlobalBatchItem = new BatchItemViewModel("__global__", this);
                // Events
                AutoUpdater.IncludeSubdirectories = true;
                AutoUpdater.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
                // AutoUpdater.Changed += new FileSystemEventHandler(OnRefreshFiles);           // Not sure we really need to monitor internal/attributes on files modifications...
                AutoUpdater.Created += new FileSystemEventHandler(OnRefreshFiles);
                AutoUpdater.Deleted += new FileSystemEventHandler(OnRefreshFiles);
                AutoUpdater.Renamed += new RenamedEventHandler(OnRefreshFiles);
                AutoUpdater.Path = bakingPath;
                AutoUpdater.EnableRaisingEvents = bakingPath != null;
                RefreshBatchItems();
            }
        }
    }
}
