using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Velopack;

namespace XnormalBatcher
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static string AppFolder { get; set; } = Directory.GetCurrentDirectory();
        internal static string TemplatePath { get; set; } = Path.Combine(AppFolder, "Data", "Template.xml");
        internal static string DefaultSettingsPath { get; set; } = Path.Combine(AppFolder, "Data", "DefaultSettings.xml");        

        private void App_Startup(object sender, StartupEventArgs e)
        {            
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
        }        
    }
}
