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
            _ = MainViewModel.LastSession;
            _ = TermsViewModel.Instance;
            _ = SettingsViewModel.Instance;
            _ = BatchViewModel.Instance;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.IsLoaded = true;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MainViewModel.Instance.SaveAndClose();
        }
    }
}
