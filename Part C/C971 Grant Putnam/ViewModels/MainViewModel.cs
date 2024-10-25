using C971_Grant_Putnam.Models;
using System;
using System.Collections.Generic;
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

        public MainViewModel(DatabaseService db)
        {
            databaseService = db;
        }
    }
}
