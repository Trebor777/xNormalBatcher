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

        [JsonConstructor]
        internal Term(string _name, string _grp)
        {
            name = _name;
            group = _grp;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class TermsModel
    {
        internal static List<Term> TermsLow = FillTerms(new string[] { "L", "LP", "Low", "LowPoly", "LPoly", "LWPLY" }, Term.Groups["L"]);
        internal static List<Term> TermsHigh = FillTerms(new string[] { "H", "HP", "High", "HighPoly", "HPoly", "HGHPLY" }, Term.Groups["H"]);
        internal static List<Term> TermsCage = FillTerms(new string[] { "C", "Cage", "Cg", "cge" }, Term.Groups["C"]);
        internal static List<Term> TermsSeparator = FillTerms(new string[] { "_", "\" \"", "-", "." }, Term.Groups["S"]);
        [JsonProperty]
        public ObservableCollection<Term> Terms { get; set; }
        internal TermsModel()
        {
            Terms = new ObservableCollection<Term>();
            foreach (var term in TermsLow)
            {
                Terms.Add(term);
            }
            foreach (var term in TermsHigh)
            {
                Terms.Add(term);
            }
            foreach (var term in TermsCage)
            {
                Terms.Add(term);
            }
            foreach (var term in TermsSeparator)
            {
                Terms.Add(term);
            }
        }
        [JsonConstructor]
        internal TermsModel(ObservableCollection<Term> _terms)
        {
            Terms = _terms;
        }

        private static List<Term> FillTerms(string[] terms, string group)
        {
            var target = new List<Term>();
            foreach (var item in terms)
            {
                Term term = new Term(item, group);
                target.Add(term);
            }
            return target;
        }
    }

    internal class TermsViewModel : BaseViewModel
    {
        public static TermsViewModel Instance { get; } = new TermsViewModel();
        internal TermsModel Data;

        public ObservableCollection<Term> Terms
        {
            get => Data.Terms; set
            {
                Data.Terms = value;
                RefreshFilteredTerms();
            }
        }

        internal void RefreshFilteredTerms()
        {
            //Data.Terms = new ObservableCollection<Term>(Terms.OrderBy(t => t.Group));
            TermsLow = new ObservableCollection<Term>(Data.Terms.Where(t => t.Group == Term.Groups["L"]));
            TermsHigh = new ObservableCollection<Term>(Data.Terms.Where(t => t.Group == Term.Groups["H"]));
            TermsCage = new ObservableCollection<Term>(Data.Terms.Where(t => t.Group == Term.Groups["C"]));
            TermsSeparator = new ObservableCollection<Term>(Data.Terms.Where(t => t.Group == Term.Groups["S"]));
            NotifyPropertyChanged("Terms");
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
            Data = MainViewModel.LastSession?.Terms ?? new TermsModel();
            RefreshFilteredTerms();
            Terms.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler((sender, arg) =>
            {
                RefreshFilteredTerms();
            }
            );
        }
    }
}
