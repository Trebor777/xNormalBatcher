using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XnormalBatcher.Helpers;

namespace XnormalBatcher.ViewModels
{
    [Serializable]
    internal class MeshSettingsLow
    {
        public object LowSmoothNormals;
        public double? LowMeshScale;
        public double? LowUOffset;
        public double? LowVOffset;
        public double? LowMeshFrontRayDistance;
        public double? LowMeshRearRayDistance;
        public string HighPolyOverrideFile;
        public string BlockerFile;
        public bool? HighPolyOverrideIsTangent;
        public bool? LowMatchUV;
        public bool? LowBatchProtection;
        public double? LowOffsetX;
        public double? LowOffsetY;
        public double? LowOffsetZ;
    }
    internal class MeshSettingsLowVM : BaseViewModel
    {
        [NonSerialized]
        public BatchItemViewModel Owner;
        private MeshSettingsLow data;
        public object LowSmoothNormals { get; internal set; }
        public double? LowMeshScale { get; internal set; }
        public double? LowUOffset { get; internal set; }
        public double? LowVOffset { get; internal set; }
        public double? LowMeshFrontRayDistance { get; internal set; }
        public double? LowMeshRearRayDistance { get; internal set; }
        public string HighPolyOverrideFile { get; internal set; }
        public string BlockerFile { get; internal set; }
        public bool? HighPolyOverrideIsTangent { get; internal set; }
        public bool? LowMatchUV { get; internal set; }
        public bool? LowBatchProtection { get; internal set; }
        public double? LowOffsetX { get; internal set; }
        public double? LowOffsetY { get; internal set; }
        public double? LowOffsetZ { get; internal set; }

        internal MeshSettingsLowVM()
        {
            data = new MeshSettingsLow();
        }

        internal MeshSettingsLowVM(MeshSettingsLowVM dataIn)
        {
            data = dataIn.data.Clone();
        }
    }
}
