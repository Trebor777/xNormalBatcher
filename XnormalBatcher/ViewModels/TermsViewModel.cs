using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XnormalBatcher.ViewModels
{
    // Holds information/logic on what are the usable terms in the different settings...
    internal class TermsViewModel : BaseViewModel
    {
        public static TermsViewModel Instance { get; } = new TermsViewModel();
        public ObservableCollection<string> TermsSeparator { get; set; }
        public ObservableCollection<string> TermsLow { get; set; }
        public ObservableCollection<string> TermsHigh { get; set; }
        public ObservableCollection<string> TermsCage { get; set; }
        public ICommand AddTerm { get; set; }
        public string NewSeparatorTerm { get; set; }
        public string NewLowTerm { get; set; }
        public string NewHighTerm { get; set; }
        public string NewCageTerm { get; set; }
        private string mSelectedEditTermSeparator;
        public string SelectedEditTermSeparator
        {
            get
            {
                return mSelectedEditTermSeparator;
            }
            set
            {
                if (mSelectedEditTermSeparator == value)
                    return;
                mSelectedEditTermSeparator = value;
                NotifyPropertyChanged();
                NewSeparatorTerm = value;
                NotifyPropertyChanged("NewLowTerm");
            }
        }
        private string mSelectedEditTermLow;
        public string SelectedEditTermLow
        {
            get
            {
                return mSelectedEditTermLow;
            }
            set
            {
                if (mSelectedEditTermLow == value)
                    return;
                mSelectedEditTermLow = value;
                NotifyPropertyChanged();
                NewLowTerm = value;
                NotifyPropertyChanged("NewLowTerm");
            }
        }
        private string mSelectedEditTermHigh;
        public string SelectedEditTermHigh
        {
            get
            {
                return mSelectedEditTermHigh;
            }
            set
            {
                if (mSelectedEditTermHigh == value)
                    return;
                mSelectedEditTermHigh = value;
                NewHighTerm = value;
                NotifyPropertyChanged("SelectedEditTermHigh");
            }
        }
        private string mSelectedEditTermCage;
        public string SelectedEditTermCage
        {
            get
            {
                return mSelectedEditTermCage;
            }
            set
            {
                if (mSelectedEditTermCage == value)
                    return;
                mSelectedEditTermCage = value;
                NewCageTerm = value;
                NotifyPropertyChanged("SelectedEditTermCage");
            }
        }

        private TermsViewModel()
        {            
            TermsLow = new ObservableCollection<string>() { "L", "LP", "Low", "LowPoly", "LPoly", "LWPLY" };
            TermsHigh = new ObservableCollection<string>() { "H", "HP", "High", "HighPoly", "HPoly", "HGHPLY" };
            TermsCage = new ObservableCollection<string>() { "C", "Cage", "Cg", "cge" };
            TermsSeparator = new ObservableCollection<string>() { "_", "\" \"", "-", "." };
        }
    }
}
