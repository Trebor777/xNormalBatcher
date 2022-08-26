using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using XnormalBatcher.Helpers;
using XnormalBatcher.ViewModels;

namespace XnormalBatcher
{
    /// <summary>
    /// Interaction logic for WindowHighPoly.xaml
    /// </summary>
    public partial class WindowHighPoly : Window
    {

        internal WindowHighPoly(MeshSettingsHighVM highSettings)
        {
            InitializeComponent();
            DataContext = highSettings;
            highSettings.PrepareDataChange();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void EdtBaseTexturePath_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(edtBaseTexturePath.Text) && !string.IsNullOrEmpty(edtBaseTexturePath.Text))
            {
                edtBaseTexturePath.Text = "";
                MessageBox.Show("Invalid Path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MeshSettingsHighVM).RevertDataChange();
            DialogResult = false;
        }
    }
}
