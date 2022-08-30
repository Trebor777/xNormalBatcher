using System;
using Squirrel;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

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
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: OnAppInstall,
                onAppUninstall: OnAppUninstall,
                onEveryRun: OnAppRun);

            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
        }

        private static void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
        {
            tools.SetProcessAppUserModelId();
            // show a welcome message when the app is first installed
            if (firstRun) MessageBox.Show("Thanks for installing my application!");
            Task.Run(() => UpdateMyApp());
        }

        private static async Task UpdateMyApp()
        {
            using (var mgr = new UpdateManager("https://xnormal.trebor777.net/"))
            {
                var newVersion = await mgr.UpdateApp();

                // optionally restart the app automatically, or ask the user if/when they want to restart
                if (newVersion != null)
                {
                    MessageBox.Show("A new version has been installed, restarting...");
                    UpdateManager.RestartApp();
                }
            }
        }
    }
}
