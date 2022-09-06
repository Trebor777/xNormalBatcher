using System.Windows;
using XnormalBatcher.ViewModels;

namespace XnormalBatcher
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            MainViewModel.Instance.Initialized = false;
            _ = MainViewModel.LastSession;
            _ = TermsViewModel.Instance;
            _ = SettingsViewModel.Instance;
            _ = BatchViewModel.Instance;
            MainViewModel.Instance.Initialized = true;
            InitializeComponent();
            BatchViewModel.Instance.InitializeData(SettingsViewModel.Instance.BakingPath);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.IsLoaded = true;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MainViewModel.Instance.SaveSession();
        }
    }
}
