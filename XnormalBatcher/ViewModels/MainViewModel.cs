using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnormalBatcher.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance;
        public BatchViewModel BatchViewModel { get; set; }
        public SettingsViewModel Settings { get; set; }
        public SuffixViewModel SuffixSettings { get; set; }


        public MainViewModel()
        {
            Instance = this;
            BatchViewModel = new BatchViewModel();
            Settings = new SettingsViewModel();
            SuffixSettings = new SuffixViewModel();
        }
    }
}
