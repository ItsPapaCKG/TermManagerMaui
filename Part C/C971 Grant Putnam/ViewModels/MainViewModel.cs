using C971_Grant_Putnam.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DatabaseService databaseService;

        public string TestBinding;

        public IEnumerable<Term> terms;

        private ObservableCollection<string> termNames;

        public ObservableCollection<string> TermNames
        {
            get { return termNames; }
            set { termNames = value; OnPropertyChanged(); }
        }


        public MainViewModel(DatabaseService db)
        {
            databaseService = db;
            databaseService.LoadSampleData();

            PopulateData();
        }

        public async void PopulateData()
        {
            terms = (List<Term>)await databaseService.GetTerms();
            TermNames = new ObservableCollection<string>();


            foreach (Term term in terms)
            {
                TermNames.Add(term.Name);
            }
        }
    }
}
