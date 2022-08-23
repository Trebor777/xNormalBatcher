using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    // Holds information/logic on how to batch, which files to batch, etc...
    internal class BatchViewModel : BaseViewModel
    {
        public ObservableCollection<int> MapSizes { get; set; }
        public ObservableCollection<string> MeshFileFormats { get; set; }
        public ObservableCollection<BatchItemViewModel> BatchItems { get; set; } = new ObservableCollection<BatchItemViewModel>();
        public BatchItemViewModel SelectedBatchItem { get; set; }
        public bool UseCage { get => useCage; set { useCage = value; RefreshBatchItems(); } }
        public bool UseTermsAsPrefix { get => useTermsAsPrefix; set { useTermsAsPrefix = value; RefreshBatchItems(); } }
        public string SelectedTermSeparator { get => selectedTermSeparator; set { selectedTermSeparator = value; RefreshBatchItems(); } }
        public string SelectedTermLow { get => selectedTermLow; set { selectedTermLow = value; RefreshBatchItems(); } }
        public string SelectedTermHigh { get => selectedTermHigh; set { selectedTermHigh = value; RefreshBatchItems(); } }
        public string SelectedTermCage { get => selectedTermCage; set { selectedTermCage = value; RefreshBatchItems(); } }
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

        public BatchViewModel()
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
            BatchItems.Add(new BatchItemViewModel());

            CMDOpenFolder = new RelayCommand(OpenFolder);
            CMDBakeSelected = new RelayCommand(BakeSelected);
            CMDBakeAll = new RelayCommand(BakeAll);


        }

        private void OpenFolder()
        {

        }
        private void BakeSelected()
        {

        }
        private void BakeAll()
        {

        }

        /// <summary>
        /// Look for files in baking folders, according to choosen naming convention (suffix, extension etc...) 
        /// </summary>
        /// <returns></returns>
        private string[] GetRefFolderFiles()
        {
            var path = AutoUpdater.Path;
            string[] files_L = FileHelper.GetItems(path + FileHelper.SubFolders[0], SelectedMeshFormatLow, FileHelper.CreateSuffix(SelectedTermLow, SelectedTermSeparator, UseTermsAsPrefix));
            string[] files_H = FileHelper.GetItems(path + FileHelper.SubFolders[1], SelectedMeshFormatHigh, FileHelper.CreateSuffix(SelectedTermHigh, SelectedTermSeparator, UseTermsAsPrefix));
            string[] files_C = FileHelper.GetItems(path + FileHelper.SubFolders[2], SelectedMeshFormatCage, FileHelper.CreateSuffix(SelectedTermCage, SelectedTermSeparator, UseTermsAsPrefix));
            List<string[]> mains = new List<string[]>() { files_L, files_H, files_C };
            //FileLowCount = files_L.Length;
            FileHighCount = files_H.Length;
            FileCageCount = UseCage ? files_C.Length : 0;
            return mains.Aggregate((i1, i2) => i1.Length > i2.Length ? i1 : i2);
        }
        /// <summary>
        /// Regenerates the list of bake objects
        /// </summary>
        private void RefreshBatchItems()
        {
            BatchItems.Clear();
            foreach (string item in GetRefFolderFiles())
            {
                var data = new BatchItemViewModel();//(item, index, true);
                //data.validate();
                BatchItems.Add(data);
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
                //if (IsLoaded && setInhibit)
                //    RefreshBatchItems();
                RefreshBatchItems();
            }
        }
    }
}
