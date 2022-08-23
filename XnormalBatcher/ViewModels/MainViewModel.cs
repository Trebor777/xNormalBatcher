using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XnormalBatcher.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance;
        public BatchViewModel BatchViewModel { get; set; }
        public SettingsViewModel Settings { get; set; }
        public SuffixViewModel SuffixSettings { get; set; }

        public string Title
        {
            get
            {
                Version version = Assembly.GetEntryAssembly().GetName().Version;
                return $"XNormal Batcher {version.ToString(3)} by trebor777.art@outlook.com";
            }
        }
        public MainViewModel()
        {
            Instance = this;
            BatchViewModel = new BatchViewModel();
            Settings = new SettingsViewModel();
            SuffixSettings = new SuffixViewModel();

            BatchViewModel.SelectedTermSeparator = SuffixSettings.TermsSeparator[0];
            BatchViewModel.SelectedTermLow = SuffixSettings.TermsLow[0];
            BatchViewModel.SelectedTermHigh = SuffixSettings.TermsHigh[0];
            BatchViewModel.SelectedTermCage = SuffixSettings.TermsCage[0];
            

            BatchViewModel.AutoUpdater.IncludeSubdirectories = true;
            BatchViewModel.AutoUpdater.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            BatchViewModel.AutoUpdater.Changed += new FileSystemEventHandler(BatchViewModel.OnRefreshFiles);
            BatchViewModel.AutoUpdater.Created += new FileSystemEventHandler(BatchViewModel.OnRefreshFiles);
            BatchViewModel.AutoUpdater.Deleted += new FileSystemEventHandler(BatchViewModel.OnRefreshFiles);
            BatchViewModel.AutoUpdater.Renamed += new RenamedEventHandler(BatchViewModel.OnRefreshFiles);
            BatchViewModel.AutoUpdater.Path = Settings.BakingPath;
            BatchViewModel.AutoUpdater.EnableRaisingEvents = Settings.BakingPath != null;
        }
    }
}
