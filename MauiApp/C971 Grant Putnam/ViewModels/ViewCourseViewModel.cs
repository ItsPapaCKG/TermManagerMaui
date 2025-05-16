using C971_Grant_Putnam.Models;
using C971_Grant_Putnam.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("SelectedCourse", "SelectedCourse")]
    [QueryProperty("Assessments", "Assessments")]
    public partial class ViewCourseViewModel
    {

        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private int carouselPosition;

        [ObservableProperty]
        private Action<int> carouselAction;

        [ObservableProperty]
        private ObservableCollection<Assessment> assessments;

        private DatabaseService database;

        private MainViewModel mainview;

        public ViewCourseViewModel(DatabaseService db, MainViewModel mvm)
        {
            database = db;
            mainview = mvm;

            if (Assessments is null)
            {
                Assessments = new();
            }
        }

        [RelayCommand]
        async Task GoToEditCourse(Course course)
        {
            if (CarouselPosition == 0)
            {
                await Shell.Current.GoToAsync($"{nameof(AddEditCourse)}", true,
                    new Dictionary<string, object> {
                    {"SelectedCourse", course},
                    {"EditMode", true}
                    });

                return;
            }

            var a = Assessments.Where(a => a.CourseId == course.Id).ToList();

            if (a.Count > 2)
            {
                throw new Exception("More than two assessments found for this course.");
            }

            var assessments = new ObservableCollection<Assessment>();
            var obj = a.FirstOrDefault(x => x.Type == "OA");
            var per = a.FirstOrDefault(x => x.Type == "PA");

            if (obj is not null)
            {
                assessments.Add(obj);
            }

            if (per is not null)
            {
                assessments.Add(per);
            }

            await Shell.Current.GoToAsync($"{nameof(EditAssessments)}", true,
                    new Dictionary<string, object> {
                    {"Assessments", assessments},
                        {"CourseId", SelectedCourse.Id}
                    }).ConfigureAwait(false);

        }

        [RelayCommand]
        async Task DeleteCourse(Course course)
        {
            await database.RemoveCourse(course);
            mainview.RefreshCourses();

            await Shell.Current.GoToAsync("..", true);

        }

        [RelayCommand]
        async Task SwitchPosition(string position)
        {
            int index = Int32.Parse(position);

            CarouselPosition = index;

            CarouselAction(index);
        }

        [RelayCommand(CanExecute = nameof(CanShareNotes))]
        public async Task ShareNotes(string notes)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = $"Check out my course notes for {SelectedCourse.Name}:\n{notes}",
                Title = "Share notes:"
            });
        }
        public bool CanShareNotes(string notes)
        {
            return !string.IsNullOrEmpty(notes ?? "");
        }
    }
}
