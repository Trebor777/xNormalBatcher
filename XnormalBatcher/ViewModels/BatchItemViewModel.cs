using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XnormalBatcher.ViewModels
{
    class BatchItemViewModel : BaseViewModel
    {
        public bool Selected { get; set; }
        public string Name { get; set; }
        public bool MultipleHP { get; set; }
        public bool Baked { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool HasLow { get; set; }
        public bool HasHigh { get; set; }
        public bool HasCage { get; set; }

        public ICommand BakeMe { get; set; }
    }
}
