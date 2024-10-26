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

        private ObservableCollection<Term> terms;

        public ObservableCollection<Term> Terms
        {
            get { return terms; }
            set { terms = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Course> courses;

        public ObservableCollection<Course> Courses
        {
            get { return courses; }
            set { courses = value; }
        }

        private ObservableCollection<Course> viewedCourses;

        public ObservableCollection<Course> ViewedCourses
        {
            get { return viewedCourses; }
            set { viewedCourses = value; }
        }



        public MainViewModel(DatabaseService db)
        {
            databaseService = db;
            databaseService.LoadSampleData();

            PopulateData();
        }

        public async void PopulateData()
        {
            Terms = await databaseService.GetTerms();
            Courses = await databaseService.GetCourses();
        }
    }
}
