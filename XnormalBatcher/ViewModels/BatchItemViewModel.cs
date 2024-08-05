using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using XnormalBatcher.Helpers;
using System.ComponentModel;
using System.Diagnostics;
using CliWrap;

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
        private string[] hpList = Array.Empty<string>();

        public bool IsSelected { get => isSelected; set { isSelected = value; NotifyPropertyChanged(); } }
        public string Name
        {
            get => name; set { name = value; NotifyPropertyChanged(); }
        }
        public bool MultipleHP { get => multipleHP; set { multipleHP = value; NotifyPropertyChanged(); Validate(); } }
        public bool Baked { get => baked; set { baked = value; NotifyPropertyChanged(); } }
        public int Width { get => width; set { width = value; NotifyPropertyChanged(); } }
        public int Height { get => height; set { height = value; NotifyPropertyChanged(); } }
        public bool HasLow { get => hasLow; set { hasLow = value; NotifyPropertyChanged(); } }
        public bool HasHigh { get => hasHigh; set { hasHigh = value; NotifyPropertyChanged(); } }
        public bool HasCage { get => hasCage; set { hasCage = value; NotifyPropertyChanged(); } }
        public bool IsValid => HasLow && HasHigh && (HasCage || !BatchViewModel.Instance.UseCage);
        public ICommand BakeMe { get; set; }
        public ICommand CMDSetLow { get; set; }
        public ICommand CMDSetHigh { get; set; }
        public MeshSettingsLowVM SettingsLow { get; set; }
        public MeshSettingsHighVM SettingsHigh { get; set; }
        private readonly BatchViewModel owner;
        private string BasenamePath => SettingsViewModel.Instance.BakingPath + Name;
        private string BasenameMapPath
        {
            get
            {
                if (owner.BakeSeparately)
                    return $@"{GetFilename(FileHelper.SubFolders[(uint)FileHelper.Slot.Maps], FileHelper.Slot.Maps, true, false)}";
                else
                    return SettingsViewModel.Instance.BakingPath + FileHelper.SubFolders[3];
            }
        }

        private string OutputXmlFile => $"{BasenamePath}.xml";
        private string OutputXmlVCFile => $"{BasenamePath}.VertexColors.xml";
        private string OutputXmlOSFile => $"{BasenamePath}.ObjectSpaceNormals.xml";

        public BatchItemViewModel(string filename, BatchViewModel Owner = null)
        {
            owner = Owner ?? BatchViewModel.Instance;
            BakeMe = new RelayCommand(Bake);
            CMDSetLow = new RelayCommand(SetLowSettings);
            CMDSetHigh = new RelayCommand(SetHighSettings);
            IsSelected = true;
            Width = owner.SelectedMapWidthAll;
            Height = owner.SelectedMapHeightAll;

            var ps0 = owner.GetMeshSuffix(FileHelper.Slot.HP);
            var ps1 = owner.GetMeshSuffix(FileHelper.Slot.LP);
            var ps2 = owner.GetMeshSuffix(FileHelper.Slot.Cage);

            string temp = Path.GetFileNameWithoutExtension(filename);
            temp = Regex.Replace(temp, ps0, "", RegexOptions.IgnoreCase);
            temp = Regex.Replace(temp, ps1, "", RegexOptions.IgnoreCase);
            temp = Regex.Replace(temp, ps2, "", RegexOptions.IgnoreCase);
            Name = temp;

            SettingsLow = new MeshSettingsLowVM() { Owner = this };
            SettingsHigh = new MeshSettingsHighVM() { Owner = this };
            Validate();
        }

        private void SetLowSettings()
        {
            Window dlg = new WindowLowPoly(SettingsLow)
            {
                Owner = Application.Current.MainWindow
            };
            dlg.ShowDialog();
            GenerateXml();
        }
        private void SetHighSettings()
        {
            Window dlg = new WindowHighPoly(SettingsHigh)
            {
                Owner = Application.Current.MainWindow
            };
            dlg.ShowDialog();
            GenerateXml();
        }

        private string PrepareBake()
        {
            GenerateXml();
            if (!Directory.Exists(BasenameMapPath) && BatchViewModel.Instance.BakeSeparately)
            {
                Directory.CreateDirectory(BasenameMapPath);
            }

            string args = $"\"{OutputXmlFile}\"";
            if (File.Exists(OutputXmlVCFile))
            {
                args += $" \"{OutputXmlVCFile}\"";
            }
            if (File.Exists(OutputXmlOSFile))
            {
                args += $" \"{OutputXmlOSFile}\"";
            }
            return args;
        }

        internal int BakeWorker()
        {
            if (!IsValid)
            {                
                return -1;
            }
            int result;
            using (var process = new Process())
            {
                process.StartInfo.FileName = SettingsViewModel.Instance.XNormalPath;
                process.StartInfo.Arguments = PrepareBake();
                process.Start();
                process.WaitForExit();
                result = process.ExitCode;
            }
            return result;
        }


        internal async void BakeAsync()
        {
            if (!IsValid)
            {
                BatchViewModel.Instance.Log($"ERROR: Asset can't be baked: {Name}");
                return;
            }
            var result = await Cli.Wrap(SettingsViewModel.Instance.XNormalPath)
                .WithArguments(PrepareBake())
                .WithValidation(CommandResultValidation.None)
                .ExecuteAsync();
            if (result.ExitCode == 1)
            {
                BatchViewModel.Instance.Log($"ERROR: User aborted or An error has occured{ (!BatchViewModel.Instance.UseCage ? "" : " (probably Cage different from lowpoly mesh)")} with asset: {Name}");
            }
            else
            {
                BatchViewModel.Instance.Log($"INFO: {Name}'s maps have been baked successfully!");
                Validate();
                IsSelected = false;
            }
        }

        private void Bake()
        {
            BatchViewModel.Instance.IsBaking = true;
            BakeAsync();
            BatchViewModel.Instance.IsBaking = false;
        }
        private string GetFilename(string typePath, FileHelper.Slot slot, bool addpath = true, bool addExtension = true, string baseName = null)
        {
            baseName ??= Name;
            string filename = baseName;
            if (slot != FileHelper.Slot.Maps)
            {
                string meshSuffix = owner.GetMeshSuffix(slot);
                filename = baseName + meshSuffix; // Default case: suffix is a suffix
                if (owner.UseTermsAsPrefix) // else, if suffix is used as prefix:
                {
                    filename = meshSuffix + baseName;
                }
            }
            string final = filename;
            if (addpath)
                final = Path.Combine(SettingsViewModel.Instance.BakingPath, typePath, filename);
            if (addExtension)
                final += "." + owner.SelectedFormats[(int)slot];
            return final;
        }

        public bool CheckHighPolyFolder()
        {
            bool hasFiles = false;
            string hp = GetFilename("", FileHelper.Slot.HP, false, true, "*");
            string path = $@"{GetFilename(FileHelper.SubFolders[(int)FileHelper.Slot.HP], FileHelper.Slot.HP, true, false)}\";
            if (Directory.Exists(path))
            {
                hpList = Directory.GetFiles(path, hp);
                hasFiles = hpList.Length > 0;
            }
            return hasFiles;
        }
        public void Validate()
        {
            HasLow = File.Exists(GetFilename(FileHelper.SubFolders[(int)FileHelper.Slot.LP], FileHelper.Slot.LP));
            HasHigh = (!MultipleHP && File.Exists(GetFilename(FileHelper.SubFolders[(int)FileHelper.Slot.HP], FileHelper.Slot.LP))) || (CheckHighPolyFolder() && MultipleHP);                        
            HasCage = !owner.UseCage || (owner.UseCage && File.Exists(GetFilename(FileHelper.SubFolders[(int)FileHelper.Slot.Cage], FileHelper.Slot.Cage)));
            Baked = false;
            if (Directory.Exists(BasenameMapPath))
                Baked = Directory.GetFiles(BasenameMapPath, Name + "_*." + owner.SelectedFormats[(int)FileHelper.Slot.Maps]).Length > 0;
            NotifyPropertyChanged("IsValid");
        }

        internal void GenerateXml(string file = null)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }
            XmlDocument document = new XmlDocument();
            document.Load(App.TemplatePath);
            var settings = SettingsViewModel.Instance;
            var rootElement = document.DocumentElement;
            var lowPolyMesh = rootElement["LowPolyModel"]["Mesh"];
            var highMeshes = rootElement["HighPolyModel"];
            var highMesh = highMeshes["Mesh"];
            var genMaps = rootElement["GenerateMaps"];

            XmlElement xnbData = document.CreateElement("xNormalBatcherData");
            xnbData.SetAttribute("Selected", IsSelected.ToString());
            xnbData.SetAttribute("UseMultipleHP", MultipleHP.ToString());
            rootElement.AppendChild(xnbData);

            highMeshes.RemoveChild(highMesh);
            if (MultipleHP)
            {
                CheckHighPolyFolder();
                foreach (var hp in hpList)
                {
                    SettingsHigh.AppendToXML(highMeshes, document, hp);
                }
            }
            else
            {
                SettingsHigh.AppendToXML(highMeshes, document, GetFilename(FileHelper.SubFolders[(int)FileHelper.Slot.HP], FileHelper.Slot.HP));
            }

            SettingsLow.SetXml(lowPolyMesh, GetFilename(FileHelper.SubFolders[0], 0), GetFilename(FileHelper.SubFolders[(int)FileHelper.Slot.Cage], FileHelper.Slot.Cage));
            genMaps.SetAttribute("Width", Width.ToString());
            genMaps.SetAttribute("Height", Height.ToString());
            genMaps.SetAttribute("File", $@"{BasenameMapPath}\{Name}.{settings.SelectedTextureFileFormat}");
            genMaps.SetAttribute("BakeHighpolyVCols", "false"); // Force off then use settings to generate specific file for vertex colors

            lowPolyMesh.SetAttribute("UseCage", $"{BatchViewModel.Instance.UseCage}");
            genMaps.SetAttribute("EdgePadding", $"{settings.EdgePadding}");
            genMaps.SetAttribute("AA", $"{settings.SelectedAASize}");
            genMaps.SetAttribute("BucketSize", $"{settings.SelectedBucketSize}");
            genMaps.SetAttribute("ClosestIfFails", $"{settings.ClosestHitRayFails}".ToLower());
            genMaps.SetAttribute("DiscardRayBackFacesHits", $"{settings.DiscardBackFaceHit}".ToLower());

            settings.SettingsAmbient.SetXML(genMaps, settings);
            settings.SettingsBaseTexture.SetXML(genMaps, settings);
            settings.SettingsBentNormal.SetXML(genMaps, settings);
            settings.SettingsCavity.SetXML(genMaps, settings);
            settings.SettingsConvexity.SetXML(genMaps, settings);
            settings.SettingsCurvature.SetXML(genMaps, settings);
            settings.SettingsDerivative.SetXML(genMaps, settings);
            settings.SettingsDirection.SetXML(genMaps, settings);
            settings.SettingsHeight.SetXML(genMaps, settings);
            settings.SettingsNormal.SetXML(genMaps, settings);
            settings.SettingsProximity.SetXML(genMaps, settings);
            settings.SettingsPRTpn.SetXML(genMaps, settings);
            settings.SettingsRadiosity.SetXML(genMaps, settings);
            settings.SettingsTranslucency.SetXML(genMaps, settings);            
            settings.SettingsWireframe.SetXML(genMaps, settings);

            genMaps.SetAttribute("GenThickness", $"{settings.BakeThickness}".ToLower());            
            if (!string.IsNullOrEmpty(file))
                document.Save(file);
            else
                document.Save(OutputXmlFile);

            if (SettingsViewModel.Instance.BakeVertexColors) // Generate Specific xml files for VertexColor Bake (needs separate bake)
            {
                settings.SettingsVertexColors.SetXML(genMaps, settings);
                highMesh.SetAttribute("IgnorePerVertexColor", "false");
                // Disable all other map bakes
                genMaps.SetAttribute("GenDerivNM", "false");
                genMaps.SetAttribute("GenCurv", "false");
                genMaps.SetAttribute("GenRadiosityNormals", "false");
                genMaps.SetAttribute("GenDirections", "false");
                genMaps.SetAttribute("GenWireRays", "false");
                genMaps.SetAttribute("GenCavity", "false");
                genMaps.SetAttribute("GenProximity", "false");
                genMaps.SetAttribute("GenThickness", "false");
                genMaps.SetAttribute("GenConvexity", "false");
                genMaps.SetAttribute("GenPRT", "false");
                genMaps.SetAttribute("GenBent", "false");
                genMaps.SetAttribute("BakeHighpolyBaseTex", "false");
                genMaps.SetAttribute("GenHeights", "false");
                genMaps.SetAttribute("GenNormals", "false");
                genMaps.SetAttribute("GenAO", "false");
                document.Save(OutputXmlVCFile);
            }
            else
            {
                
                if (File.Exists(OutputXmlVCFile))
                    File.Delete(OutputXmlVCFile);
            }
            if (SettingsViewModel.Instance.BothNormalsType) // Generate Specific xml files when baking both normal map types (needs separate bake)
            {
                genMaps.SetAttribute("GenNormals", "true");
                genMaps.SetAttribute("TangentSpace", "false");

                genMaps.SetAttribute("File", $@"{BasenameMapPath}\{Name}_ObjectSpace.{SettingsViewModel.Instance.SelectedTextureFileFormat}");
                // Disable all other map bakes
                genMaps.SetAttribute("GenDerivNM", "false");
                genMaps.SetAttribute("GenCurv", "false");
                genMaps.SetAttribute("GenRadiosityNormals", "false");
                genMaps.SetAttribute("GenDirections", "false");
                genMaps.SetAttribute("GenWireRays", "false");
                genMaps.SetAttribute("GenCavity", "false");
                genMaps.SetAttribute("GenProximity", "false");
                genMaps.SetAttribute("GenThickness", "false");
                genMaps.SetAttribute("GenConvexity", "false");
                genMaps.SetAttribute("GenPRT", "false");
                genMaps.SetAttribute("GenBent", "false");
                genMaps.SetAttribute("BakeHighpolyBaseTex", "false");
                genMaps.SetAttribute("GenHeights", "false");
                genMaps.SetAttribute("GenAO", "false");
                genMaps.SetAttribute("BakeHighpolyVCols", "false");
                document.Save(OutputXmlOSFile);
            }
            else
            {
                if (File.Exists(OutputXmlOSFile))
                    File.Delete(OutputXmlOSFile);
            }
        }
    }
}
