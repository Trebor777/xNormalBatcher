using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XnormalBatcher.ViewModels;
using XnormalBatcher.Helpers;

namespace XnormalBatcher
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowLowPoly : Window
    {
        private ObservableCollection<string> SmoothMethods = new ObservableCollection<string>() { "Exported", "Average", "Harden" };
        internal WindowLowPoly(MeshSettingsLowVM lowSettings)
        {
            DataContext = lowSettings;
            InitializeComponent();
            cbSmoothNormal.ItemsSource = SmoothMethods;
            cbSmoothNormal.SelectedItem = lowSettings.LowSmoothNormals;
            udMeshScale.Value = lowSettings.LowMeshScale;
            udUoffSet.Value = lowSettings.LowUOffset;
            udVoffSet.Value = lowSettings.LowVOffset;
            udMaxFrontRay.Value = lowSettings.LowMeshFrontRayDistance;
            udMaxRearRay.Value = lowSettings.LowMeshRearRayDistance;
            edtHPnormals.Text = lowSettings.HighPolyOverrideFile;
            edtBlockers.Text = lowSettings.BlockerFile;
            chkHighPolyNormalsOverride.IsChecked = lowSettings.HighPolyOverrideIsTangent;
            chkMatchUV.IsChecked = lowSettings.LowMatchUV;
            chkBatchProtection.IsChecked = lowSettings.LowBatchProtection;
            udPOffsetX.Value = lowSettings.LowOffsetX;
            udPOffsetY.Value = lowSettings.LowOffsetY;
            udPOffsetZ.Value = lowSettings.LowOffsetZ;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var lowSettings = DataContext as MeshSettingsLowVM;
            lowSettings.LowMeshScale = (double)udMeshScale.Value;
            lowSettings.LowUOffset = (double)udUoffSet.Value;
            lowSettings.LowVOffset = (double)udVoffSet.Value;
            lowSettings.LowSmoothNormals = cbSmoothNormal.SelectedItem.ToString();
            lowSettings.LowMeshFrontRayDistance = (double)udMaxFrontRay.Value;
            lowSettings.LowMeshRearRayDistance = (double)udMaxRearRay.Value;
            lowSettings.LowOffsetX = (double)udPOffsetX.Value;
            lowSettings.LowOffsetY = (double)udPOffsetY.Value;
            lowSettings.LowOffsetZ = (double)udPOffsetZ.Value;
            lowSettings.HighPolyOverrideFile = edtHPnormals.Text;
            lowSettings.BlockerFile = edtBlockers.Text;
            lowSettings.HighPolyOverrideIsTangent = chkHighPolyNormalsOverride.IsChecked == true;
            lowSettings.LowMatchUV = chkMatchUV.IsChecked == true;
            lowSettings.LowBatchProtection = chkBatchProtection.IsChecked == true;
            if (lowSettings.Owner.Name != "__global__")
                lowSettings.Owner.GenerateXml();
            DialogResult = true;
        }

        private void btnBrowseHPNormals_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = FileHelper.GenerateExtensionFilter("Image Files", SettingsViewModel.Instance.TextureFileFormats.ToList());
            if (Directory.Exists(edtHPnormals.Text))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(edtHPnormals.Text);
            if (dlg.ShowDialog() == true)
            {
                edtHPnormals.Text = dlg.FileName;
            }
        }

        private void btnBrowseBlockers_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = FileHelper.GenerateExtensionFilter("All known file types", BatchViewModel.Instance.MeshFileFormats.ToList());
            if (Directory.Exists(edtBlockers.Text))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(edtBlockers.Text);
            if (dlg.ShowDialog() == true)
            {
                edtBlockers.Text = dlg.FileName;
            }
        }

        private void edtHPnormals_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(edtHPnormals.Text) && !string.IsNullOrEmpty(edtHPnormals.Text))
            {
                edtHPnormals.Text = "";
                MessageBox.Show("Invalid Path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void edtBlockers_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(edtBlockers.Text) && !string.IsNullOrEmpty(edtBlockers.Text))
            {
                edtBlockers.Text = "";
                MessageBox.Show("Invalid Path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
