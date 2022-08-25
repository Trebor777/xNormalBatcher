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
        private ObservableCollection<string> SmoothMethods = new ObservableCollection<string>() { "Exported", "Average", "Harden" };
        internal WindowHighPoly(MeshSettingsHighVM highSettings)
        {                        
            InitializeComponent();
            DataContext = highSettings;
            cbSmoothNormal.ItemsSource = SmoothMethods;
            udMeshScale.Value = highSettings.HighMeshScale;
            edtBaseTexturePath.Text = highSettings.HighPolyBaseTexture;
            cbSmoothNormal.SelectedItem = highSettings.HighSmoothNormals;
            chkBaseIsTangent.IsChecked = highSettings.HighPolyBaseTextureIsTangent;
            chkIgnoreVColors.IsChecked = highSettings.IgnoreVertexColor;
            udPOffsetX.Value = highSettings.HighOffsetX;
            udPOffsetY.Value = highSettings.HighOffsetY;
            udPOffsetZ.Value = highSettings.HighOffsetZ;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var highSettings = DataContext as MeshSettingsHighVM;
            highSettings.HighMeshScale = (double)udMeshScale.Value;
            highSettings.HighPolyBaseTexture = edtBaseTexturePath.Text;
            highSettings.HighSmoothNormals = cbSmoothNormal.SelectedItem.ToString();
            highSettings.HighPolyBaseTextureIsTangent = chkBaseIsTangent.IsChecked == true;
            highSettings.IgnoreVertexColor = chkIgnoreVColors.IsChecked == true;
            highSettings.HighOffsetX = (double)udPOffsetX.Value;
            highSettings.HighOffsetY = (double)udPOffsetY.Value;
            highSettings.HighOffsetZ = (double)udPOffsetZ.Value;
            if (highSettings.Owner.Name != "__global__")
                highSettings.Owner.GenerateXml();
            DialogResult = true;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();            
            dlg.Filter = FileHelper.GenerateExtensionFilter("Image Files", SettingsViewModel.Instance.TextureFileFormats.ToList());
            if (File.Exists(edtBaseTexturePath.Text))
                dlg.InitialDirectory = Path.GetDirectoryName(edtBaseTexturePath.Text);
            if (dlg.ShowDialog() == true)
            {
                edtBaseTexturePath.Text = dlg.FileName;
            }
        }

        private void edtBaseTexturePath_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(edtBaseTexturePath.Text) && !string.IsNullOrEmpty(edtBaseTexturePath.Text))
            {
                edtBaseTexturePath.Text = "";
                MessageBox.Show("Invalid Path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
