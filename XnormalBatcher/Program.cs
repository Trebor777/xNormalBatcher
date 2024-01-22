using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Velopack;
using XnormalBatcher.Helpers;

namespace XnormalBatcher
{
    internal class Program
    {
        internal static ILogger logger;

        [STAThread]
        public static void Main(string[] args)
        {
            //Define the path to the text file
            string logFilePath = "xNormalBatcher_log.log";

            //Create a StreamWriter to write logs to a text file
            using (StreamWriter logFileWriter = new StreamWriter(logFilePath, append: true))
            {
                //Create an ILoggerFactory
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {                    

                    //Add a custom log provider to write logs to text files
                    builder.AddProvider(new CustomFileLoggerProvider(logFileWriter));
                });

                //Create an ILogger
                logger = loggerFactory.CreateLogger<Program>();

                VelopackApp.Build()
                .WithFirstRun(v => MessageBox.Show("Thanks for installing my application!"))
                .Run(logger);

                var t = Task.Run(() => UpdateMyApp());
                t.Wait();


                var application = new App();
                application.InitializeComponent();
                application.Run();
            }


            
        }

        private static async Task UpdateMyApp()
        {
            var mgr = new UpdateManager("https://xnormal.trebor777.net/", logger: logger);
            if (!mgr.IsInstalled)
                return;
            var newVersion = await mgr.CheckForUpdatesAsync().ConfigureAwait(true);
            if (newVersion == null)
                return;

            await mgr.DownloadUpdatesAsync(newVersion).ConfigureAwait(true);
            MessageBox.Show("A new version will been installed, restarting...");
            mgr.ApplyUpdatesAndRestart();
        }

        internal static string GetVersion()
        {
            var mgr = new UpdateManager("https://xnormal.trebor777.net/", logger: logger);
            if (!mgr.IsInstalled)
                return "0.0.0";
            else
                return mgr.CurrentVersion.ToString();
        }
    }
}
