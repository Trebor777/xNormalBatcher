using Newtonsoft.Json;
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
    internal class Term
    {
        public static Dictionary<string, string> Groups = new Dictionary<string, string>() { ["L"] = "Low", ["H"] = "High", ["C"] = "Cage", ["S"] = "Separator" };
        private string name;
        private string group;

        public string Name
        {
            get => name; set
            {
                name = value;
                TermsViewModel.Instance?.RefreshFilteredTerms();
            }
        }
        public string Group
        {
            get => group; set
            {
                group = value;
                TermsViewModel.Instance?.RefreshFilteredTerms();
            }
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class TermsViewModel : BaseViewModel
    {
        public static TermsViewModel Instance { get; } = new TermsViewModel();
        private ObservableCollection<Term> terms;
        [JsonProperty]
        public ObservableCollection<Term> Terms
        {
            get => terms; set
            {
                terms = value;
                RefreshFilteredTerms();
            }
        }

        internal void RefreshFilteredTerms()
        {
            TermsLow = new ObservableCollection<Term>(terms.Where(t => t.Group == Term.Groups["L"]));
            TermsHigh = new ObservableCollection<Term>(terms.Where(t => t.Group == Term.Groups["H"]));
            TermsCage = new ObservableCollection<Term>(terms.Where(t => t.Group == Term.Groups["C"]));
            TermsSeparator = new ObservableCollection<Term>(terms.Where(t => t.Group == Term.Groups["S"]));
            NotifyPropertyChanged();
            NotifyPropertyChanged("TermsLow");
            NotifyPropertyChanged("TermsHigh");
            NotifyPropertyChanged("TermsCage");
            NotifyPropertyChanged("TermsSeparator");

        }

        public ObservableCollection<string> TermGroups => new ObservableCollection<string>(Term.Groups.Values);
        public ObservableCollection<Term> TermsSeparator { get; set; }
        public ObservableCollection<Term> TermsLow { get; set; }
        public ObservableCollection<Term> TermsHigh { get; set; }
        public ObservableCollection<Term> TermsCage { get; set; }
        public ICommand CMDAddTerm { get; set; }
        public string NewTerm { get; set; }


        private TermsViewModel()
        {
            Terms = new ObservableCollection<Term>();
            Terms.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler((sender, arg) =>
            {
                RefreshFilteredTerms();
            }
            );
            foreach (var item in new string[] { "L", "LP", "Low", "LowPoly", "LPoly", "LWPLY" })
            {
                Term term = new Term() { Name = item, Group = Term.Groups["L"] };
                Terms.Add(term);
            }
            foreach (var item in new string[] { "H", "HP", "High", "HighPoly", "HPoly", "HGHPLY" })
            {
                Term term = new Term() { Name = item, Group = Term.Groups["H"] };
                Terms.Add(term);
            }
            foreach (var item in new string[] { "C", "Cage", "Cg", "cge" })
            {
                Term term = new Term() { Name = item, Group = Term.Groups["C"] };
                Terms.Add(term);
            }
            foreach (var item in new string[] { "_", "\" \"", "-", "." })
            {
                Term term = new Term() { Name = item, Group = Term.Groups["S"] };
                Terms.Add(term);
            }
        }
    }
}
