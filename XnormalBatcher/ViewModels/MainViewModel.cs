using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XnormalBatcher.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance { get; } = new MainViewModel();                

        public bool IsLoaded = false;

        public string Title
        {
            get
            {
                Version version = Assembly.GetEntryAssembly().GetName().Version;
                return $"XNormal Batcher {version.ToString(3)} by trebor777.art@outlook.com";
            }
        }
    }
}
