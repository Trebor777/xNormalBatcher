using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace XnormalBatcher.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class DataObject
    {
        [JsonProperty]
        internal BatchModel Batch { get; set; }
        [JsonProperty]
        internal SettingsModel Settings { get; set; }
        [JsonProperty]
        internal TermsModel Terms { get; set; }
    }
    
    internal class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance { get; } = new MainViewModel();

        internal static string SessionFile = $@"{Environment.CurrentDirectory}\last.dat";
        private static DataObject _lastSession;
        internal static DataObject LastSession => _lastSession ?? LoadLastSession();

        public bool IsLoaded = false;

        private bool isBaking = false;

        public bool IsBaking
        {
            get => isBaking;
            set { isBaking = value; NotifyPropertyChanged(); }
        }
        public string Title
        {
            get
            {
                Version version = Assembly.GetEntryAssembly().GetName().Version;
                return $"XNormal Batcher {version.ToString(3)} by trebor777.art@outlook.com";
            }
        }

        internal void SaveAndClose()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Include;
            using (StreamWriter sw = new StreamWriter(SessionFile))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, new DataObject { Batch = BatchViewModel.Instance.Data, Settings = SettingsViewModel.Instance.Data, Terms = TermsViewModel.Instance.Data });
                }
            }
        }

        internal static DataObject LoadLastSession()
        {
            _lastSession = null;
            if (File.Exists(SessionFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.NullValueHandling = NullValueHandling.Include;
                using (StreamReader sr = new StreamReader(SessionFile))
                {
                    _lastSession = JsonConvert.DeserializeObject<DataObject>(sr.ReadToEnd());                    
                }
            }            
            return _lastSession;
        }
    }
}
