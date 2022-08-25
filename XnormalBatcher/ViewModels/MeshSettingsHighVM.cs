using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    [Serializable]
    internal class MeshSettingsHigh
    {
        internal double HighMeshScale;
        internal string HighPolyBaseTexture;
        internal string HighSmoothNormals;
        internal bool HighPolyBaseTextureIsTangent;
        internal bool IgnoreVertexColor;
        internal double HighOffsetX;
        internal double HighOffsetY;
        internal double HighOffsetZ;
    }

    internal class MeshSettingsHighVM : BaseViewModel
    {
        public BatchItemViewModel Owner;
        private readonly MeshSettingsHigh data;
        public double HighMeshScale { get => data.HighMeshScale; set => data.HighMeshScale = value; }
        public string HighPolyBaseTexture { get => data.HighPolyBaseTexture; set => data.HighPolyBaseTexture = value; }
        public string HighSmoothNormals { get=>data.HighSmoothNormals; set => data.HighSmoothNormals=value; }
        public bool HighPolyBaseTextureIsTangent { get => data.HighPolyBaseTextureIsTangent; set => data.HighPolyBaseTextureIsTangent = value; }
        public bool IgnoreVertexColor { get => data.IgnoreVertexColor; set => data.IgnoreVertexColor = value; }
        public double HighOffsetX { get => data.HighOffsetX; set => data.HighOffsetX = value; }
        public double HighOffsetY { get => data.HighOffsetY; set => data.HighOffsetY = value; }
        public double HighOffsetZ { get => data.HighOffsetZ; set => data.HighOffsetZ = value; }

        internal MeshSettingsHighVM()
        {
            data = new MeshSettingsHigh();
        }

        internal MeshSettingsHighVM(MeshSettingsHighVM dataIn)
        {
            data = dataIn.data.Clone();
        }
    }
}
