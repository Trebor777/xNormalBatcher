using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    internal class BatchItemViewModel : BaseViewModel
    {
        private bool isSelected;
        private string name;
        private bool multipleHP;
        private bool baked;
        private int width;
        private int height;
        private bool hasLow;
        private bool hasHigh;
        private bool hasCage;        
        private string[] hpList = { };

        public bool IsSelected { get => isSelected; set { isSelected = value; NotifyPropertyChanged(); } }
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); }
}
        public bool MultipleHP { get => multipleHP; set { multipleHP = value; NotifyPropertyChanged(); } }
        public bool Baked { get => baked; set { baked = value; NotifyPropertyChanged(); } }
        public int Width { get => width; set { width = value; NotifyPropertyChanged(); } }
        public int Height { get => height; set { height = value; NotifyPropertyChanged(); } }
        public bool HasLow { get => hasLow; set { hasLow = value; NotifyPropertyChanged(); } }
        public bool HasHigh { get => hasHigh; set { hasHigh = value; NotifyPropertyChanged(); }}
        public bool HasCage { get => hasCage; set { hasCage = value; NotifyPropertyChanged(); }}
        public bool IsValid => HasLow && HasHigh && (HasCage || !BatchViewModel.Instance.UseCage);
        public ICommand BakeMe { get; set; }

        public MeshSettingsLowVM SettingsLow { get; set; }
        public MeshSettingsHighVM SettingsHigh { get; set; }

        private readonly BatchViewModel owner;



        public BatchItemViewModel(string filename)
        {
            owner = BatchViewModel.Instance;
            BakeMe = new RelayCommand(_Bake);
            IsSelected = true;
            Width = owner.SelectedMapWidthAll;
            Height = owner.SelectedMapHeightAll;

            string temp = Path.GetFileNameWithoutExtension(filename);
            var ps0 = owner.GetSuffix(0);
            var ps1 = owner.GetSuffix(1);
            var ps2 = owner.GetSuffix(2);
            temp = Regex.Replace(temp, ps0, "", RegexOptions.IgnoreCase);
            temp = Regex.Replace(temp, ps1, "", RegexOptions.IgnoreCase);
            temp = Regex.Replace(temp, ps2, "", RegexOptions.IgnoreCase);
            Name = temp;

            SettingsLow = new MeshSettingsLowVM() { Owner = this };
            SettingsHigh = new MeshSettingsHighVM() { Owner = this };
            Validate();
        }

        private void _Bake()
        {

        }

        public int Bake()
        {
            _Bake();
            return 0;
        }
        private string GenerateName(string typePath, int slot, bool addpath = true, bool addExtension = true, string name = null)
        {
            name = name ?? Name;
            string result = name;
            if (slot < 3)
            {
                string suffix = owner.GetSuffix(slot);
                result = name + suffix; // Default case: suffix is a suffix
                if (owner.UseTermsAsPrefix) // else, if suffix is used as prefix:
                {
                    result = suffix + name;
                }
            }
            string final = result;
            if (addpath)
                final = SettingsViewModel.Instance.BakingPath + typePath + result;
            if (addExtension)
                final += "." + owner.SelectedFormats[slot];
            return final;
        }

        private string GetBasenamePath()
        {
            return SettingsViewModel.Instance.BakingPath + Name;
        }

        private string GetBasenameMapPath()
        {
            if (owner.BakeSeparately)
                return GenerateName(FileHelper.SubFolders[3], 3, true, false);
            else
                return GenerateName(FileHelper.SubFolders[3], 3, true, false) + Name;
        }
        public bool CheckHighPolyFolder()
        {
            bool hasFiles = false;
            string hp = MultipleHP ? "*" : Name;
            string path = MultipleHP ? GenerateName(FileHelper.SubFolders[1], 1, true, false) + @"\" : SettingsViewModel.Instance.BakingPath + FileHelper.SubFolders[1];

            bool exists = Directory.Exists(path);
            if (exists)
            {
                hpList = Directory.GetFiles(path, GenerateName("", 1, false, true, hp));
                hasFiles = hpList.Length > 0;
            }
            return exists && hasFiles;
        }
        public void Validate()
        {            
            HasLow = File.Exists(GenerateName(FileHelper.SubFolders[0], 0));
            HasHigh = (File.Exists(GenerateName(FileHelper.SubFolders[1], 1)) && !MultipleHP) || (CheckHighPolyFolder() && MultipleHP);
            string path = GenerateName(FileHelper.SubFolders[2], 2);
            bool v = File.Exists(path);
            HasCage = (v && owner.UseCage) || !owner.UseCage;
            Baked = false;

            if (Directory.Exists(GetBasenameMapPath()))
                Baked = Directory.GetFiles(GetBasenameMapPath(), Name + "_*." + owner.SelectedFormats[3]).Length > 0;
                        
            NotifyPropertyChanged("IsValid");
        }


        internal void GenerateXml()
        {
            throw new NotImplementedException();
        }
    }
}
