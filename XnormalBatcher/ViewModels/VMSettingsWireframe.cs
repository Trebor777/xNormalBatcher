using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsWireframe
    {
        internal bool RenderWireframe;
        internal bool RenderRayFails;
        internal Color Color;
        internal Color BackgroundColor;
        internal Color CWColor;
        internal Color SeamColor;
        internal Color RayFailColor;
        internal SettingsWireframe()
        {
            RenderWireframe = true;
            RenderRayFails = true;
            Color = Color.FromRgb(255, 255, 255);
            CWColor = Color.FromRgb(0, 0, 255);
            SeamColor = Color.FromRgb(0, 255, 0);
            RayFailColor = Color.FromRgb(255, 0, 0);
            BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }
    internal class VMSettingsWireframe : BaseViewModel
    {
        private SettingsWireframe Data { get; set; }
        public bool RenderWireFrame
        {
            get => Data.RenderWireframe; set
            {
                Data.RenderWireframe = value;
                NotifyPropertyChanged();
            }
        }
        public bool RenderRayFails
        {
            get => Data.RenderWireframe; set
            {
                Data.RenderWireframe = value;
                NotifyPropertyChanged();
            }
        }
        public Color Color
        {
            get => Data.Color; set
            {
                Data.Color = value;
                NotifyPropertyChanged();
            }
        }
        public Color BackgroundColor
        {
            get => Data.BackgroundColor; set
            {
                Data.BackgroundColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color CWColor
        {
            get => Data.CWColor; set
            {
                Data.CWColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color SeamColor
        {
            get => Data.SeamColor; set
            {
                Data.SeamColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color RayFailColor
        {
            get => Data.RayFailColor; set
            {
                Data.RayFailColor = value;
                NotifyPropertyChanged();
            }
        }
        internal VMSettingsWireframe()
        {
            Data = new SettingsWireframe();
        }
    }
}
