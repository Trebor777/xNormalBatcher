using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnormalBatcher.ViewModels
{
    // Holds information/logic on how to batch, which files to batch, etc...
    internal class BatchViewModel : BaseViewModel
    {
        public bool UseCage { get; set; }
        public bool UseTermsAsPrefix { get; set; }
        public bool BakeSeparately { get; set; }
        public ObservableCollection<int> MapSizes { get; set; }
        public ObservableCollection<string> MeshFileFormats { get; set; }
        public ObservableCollection<BatchItemViewModel> BatchItems { get; set; } = new ObservableCollection<BatchItemViewModel>();
        public BatchItemViewModel SelectedBatchItem { get; set; }
        public string SelectedTermSeparator { get; set; }
        public string SelectedTermLow { get; set; }
        public string SelectedTermHigh { get; set; }
        public string SelectedTermCage { get; set; }
        public string SelectedMeshFormatLow { get; set; }
        public string SelectedMeshFormatHigh { get; set; }
        public string SelectedMeshFormatCage { get; set; }
        public int SelectedMapWidthAll { get; set; }
        public int SelectedMapHeightAll { get; set; }

        public int FileLowCount { get; set; }
        public int FileHighCount { get; set; }
        public int FileCageCount { get; set; }

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
            SelectedTermSeparator = MainViewModel.Instance.SuffixSettings.TermsSeparator[0];
            SelectedTermLow = MainViewModel.Instance.SuffixSettings.TermsLow[0];
            SelectedTermHigh = MainViewModel.Instance.SuffixSettings.TermsHigh[0];
            SelectedTermCage = MainViewModel.Instance.SuffixSettings.TermsCage[0];
            FileLowCount = FileHighCount = FileCageCount = 0;
            BatchItems.Add(new BatchItemViewModel());
        }
    }
}
