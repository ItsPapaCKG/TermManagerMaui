using C971_Grant_Putnam.Models;
using C971_Grant_Putnam.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;


namespace C971_Grant_Putnam.ViewModels
{

    [INotifyPropertyChanged]
    [QueryProperty("ShouldRefresh","ShouldRefresh")]
    public partial class MainViewModel : IQueryAttributable
    {
        private DatabaseService databaseService;

        public ObservableCollection<Term> Terms { get; } = new();

        public ObservableCollection<Course> Courses { get; } = new();

        public ObservableCollection<Course>? ViewedCourses { get; } = new();

        public ObservableCollection<Assessment> Assessments { get; } = new();

        [ObservableProperty]
        private Term selectedTerm;

        [ObservableProperty]
        private bool shouldRefresh;

        [ObservableProperty]
        private string selectedTermDateRange;

        private readonly WeakReferenceMessenger _messenger;

        public MainViewModel(DatabaseService db)
        {
            databaseService = db;

            PopulateData();

            WeakReferenceMessenger.Default.Register<UpdateTermMessage>(this, (r, m) =>
            {
                //SelectedTerm = m.Value;
                Term updatedTerm = m.Value;

                var index = Terms.IndexOf(Terms.FirstOrDefault(term => term.Id == updatedTerm.Id));

                if (index >= 0)
                {
                    Terms[index] = updatedTerm;
                    SelectedTerm = updatedTerm;

                    ViewedCourses.Clear();
                    SwitchTerm(updatedTerm);
                } else
                {
                    Terms.Add(updatedTerm);
                    SelectedTerm = updatedTerm;

                    ViewedCourses.Clear();
                    SwitchTerm(updatedTerm);
                }

            });

        }

        [RelayCommand]
        async Task GoToEditTermAsync(Term term)
        {
            if (term is null)
            {
                await Shell.Current.GoToAsync($"{nameof(AddEditTerm)}", true,
                new Dictionary<string, object>
                {
                    {"EditMode", false }
                }
                );

                return;
            }

            await Shell.Current.GoToAsync($"{nameof(AddEditTerm)}", true,
                new Dictionary<string, object>
                {
                    { "SelectedTerm", term},
                    {"EditMode", true }
                }
                );
        }

        [RelayCommand]
        public async Task AddCourse()
        {
            await Shell.Current.GoToAsync($"{nameof(AddEditCourse)}", true, new Dictionary<string, object>
            {
                {"EditMode", false },
                {"TermId", SelectedTerm.Id }
            });
        }

        [RelayCommand]
        private async void SwitchTerm(Term term)
        {
            if (term == SelectedTerm && ViewedCourses.Count != 0)
                return;

            SelectedTerm = term;
            ViewedCourses.Clear();

            App.Current.Dispatcher.Dispatch(() =>
            {

                var list = new List<Course>();

                list = Courses.Where(c => c.TermId == term.Id).ToList();

                SelectedTermDateRange = SelectedTerm.Start.ToString("MM/dd/yyyy") + " - " + SelectedTerm.End.ToString("MM/dd/yyyy");

                foreach (var course in list)
                {
                    ViewedCourses.Add(course);
                }
            });
        }

        [RelayCommand]
        async void ViewCourseInfoAsync(Course course)
        {
            var a = Assessments.Where(a => a.CourseId == course.Id).ToList();

            if (a.Count <= 2 && a.Count > 0)
            {
                var assess = new ObservableCollection<Assessment>();
                var obj = a.FirstOrDefault(x => x.Type == "OA", null);
                var per = a.FirstOrDefault(x => x.Type == "PA", null);

                if (obj is not null)
                { assess.Add(obj); }

                if (per is not null)
                { assess.Add(per); }


                    await Shell.Current.GoToAsync($"{nameof(ViewCoursePage)}", true,
                        new Dictionary<string, object>
                        {
                    { "SelectedCourse", course},
                    {"Assessments", assess}
                        }
                    ).ConfigureAwait(false);


            } else
            {
                await Shell.Current.GoToAsync($"{nameof(ViewCoursePage)}", true,
                    new Dictionary<string, object>
                    {
                    { "SelectedCourse", course}
                    }
                ).ConfigureAwait(false);
            }
        }

        public async Task RefreshCourses()
        {
            var courses = await databaseService.GetCourses();
            var assessments = await databaseService.GetAssessments();
            var terms = await databaseService.GetTerms();

            Terms.Clear();
            Courses.Clear();
            ViewedCourses.Clear();
            Assessments.Clear();

            foreach (var t in terms)
            {
                Terms.Add(t);
            }

            foreach (var c in courses)
            {
                Courses.Add(c);
            }

            foreach (var a in assessments)
            {
                Assessments.Add(a);
            }

            SwitchTerm(SelectedTerm);
        }

        public async void PopulateData()
        {
            Terms.Clear();
            Courses.Clear();

            if (/*CheckFirstLaunch()*/true)
            {
                await databaseService.LoadSampleData();
            }
            //Terms = await databaseService.GetTerms();
            var terms = await databaseService.GetTerms();
            //Courses = await databaseService.GetCourses();
            var courses = await databaseService.GetCourses();

            var assessments = await databaseService.GetAssessments();

            foreach (var term in terms)
            {
                Terms.Add(term);
            }

            foreach (var course in courses)
            {
                Courses.Add(course);
            }

            foreach (var assessment in assessments)
            {
                Assessments.Add(assessment);
            }

            if (SelectedTerm is null)
            {
                SelectedTerm = Terms[0];
            }

            App.Current.Dispatcher.Dispatch(() =>
            {
                ViewedCourses.Clear();
                SwitchTerm(SelectedTerm);

            });

        }

        public async void SwitchToTerm(int termId)
        {
            SelectedTerm = Terms.FirstOrDefault(t => t.Id == termId) is not null ? Terms.FirstOrDefault(t => t.Id == termId) : SelectedTerm;
        }

        public bool CheckFirstLaunch()
        {
            string k = "FirstLaunch";

            bool hasLaunchedBefore = Preferences.Get(k, false);

            if (!hasLaunchedBefore)
            {
                Preferences.Set(k, true);
            }

            return !hasLaunchedBefore;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("ShouldRefresh"))
            {
                PopulateData();
            }
        }
    }
}
