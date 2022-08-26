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
        internal WindowLowPoly(MeshSettingsLowVM lowSettings)
        {
            InitializeComponent();
            DataContext = lowSettings;
            lowSettings.PrepareDataChange();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MeshSettingsLowVM).RevertDataChange();
            DialogResult = false;
        }
        private void EdtHPnormals_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(edtHPnormals.Text) && !string.IsNullOrEmpty(edtHPnormals.Text))
            {
                edtHPnormals.Text = "";
                MessageBox.Show("Invalid Path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EdtBlockers_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(edtBlockers.Text) && !string.IsNullOrEmpty(edtBlockers.Text))
            {
                edtBlockers.Text = "";
                MessageBox.Show("Invalid Path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
