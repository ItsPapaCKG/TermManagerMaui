using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("SelectedCourse","SelectedCourse")]
    [QueryProperty("EditMode","EditMode")]
    [QueryProperty("TermId","TermId")]
    public partial class AddEditCourseViewModel : IQueryAttributable
    {

        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private int termId;

        [ObservableProperty]
        private Course courseEdit;

        [ObservableProperty]
        private bool editMode;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesToCourseCommand))]
        private string name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesToCourseCommand))]
        private string nameError;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesToCourseCommand))]
        private string phone;

        [ObservableProperty]
        private string phoneError;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesToCourseCommand))]
        private string email;

        [ObservableProperty]
        private string emailError;


        [ObservableProperty]
        private Dictionary<string, string> validationErrors;

        private DatabaseService database;

        private MainViewModel mainview;

        public AddEditCourseViewModel(DatabaseService db, MainViewModel mvm)
        {
            database = db;
            mainview = mvm;

            if (CourseEdit == null)
            {
                CourseEdit = new Course();
                CourseEdit.Start = DateTime.Now;
                CourseEdit.End = DateTime.Now.AddMonths(1);
            }

            CourseEdit.Start = DateTime.Now;
            CourseEdit.End = DateTime.Now.AddMonths(1);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedCourse"))
            {
                SelectedCourse = query["SelectedCourse"] as Course ?? new Course();

                CourseEdit = new Course
                {
                    Id = SelectedCourse.Id,
                    TermId = SelectedCourse.TermId,
                    Name = SelectedCourse.Name,
                    Start = SelectedCourse.Start,
                    End = SelectedCourse.End,
                    Notify = SelectedCourse.Notify,
                    Status = SelectedCourse.Status,
                    Instructor_Name = SelectedCourse.Instructor_Name,
                    Instructor_Phone = SelectedCourse.Instructor_Phone,
                    Instructor_Email = SelectedCourse.Instructor_Email,
                    Notes = SelectedCourse.Notes
                };

                Name = SelectedCourse.Instructor_Name;
                Phone = SelectedCourse.Instructor_Phone;
                Email = SelectedCourse.Instructor_Email;

            }
            else 
            {
                TermId = (int)query["TermId"];
                CourseEdit = new Course() { 
                    TermId = TermId,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMonths(1)
                };
            }

        }

        //TODO relay command to save
        [RelayCommand(CanExecute = nameof(CanSaveChanges))]
        public async Task SaveChangesToCourse(Course course)
        {
            try
            {
                if (course is null) { throw new Exception("New instance of course being edited is null."); }
                
                course.Instructor_Name = Name;
                course.Instructor_Phone = Phone;
                course.Instructor_Email = Email;

                if (EditMode && SelectedCourse is not null)
                {

                    await database.UpdateCourse(SelectedCourse.Id, course).ConfigureAwait(false);

                    course.Id = SelectedCourse.Id;
                    SelectedCourse = course;
                    mainview.RefreshCourses();

                    await Shell.Current.GoToAsync("..", true, new Dictionary<string, object> { { "SelectedCourse", course} }).ConfigureAwait(false);
                }
                else
                {
                    await database.AddCourse(course).ConfigureAwait(false);
                    mainview.RefreshCourses();
                    await Shell.Current.GoToAsync("..", true, new Dictionary<string, object> { { "SelectedCourse", course } }).ConfigureAwait(false);
                }

                await database.RemoveAllNotifications(course.Id).ConfigureAwait(false);

                if (course.Notify)
                {

                    await database.CreateNewNotification(DateTime.Now, "initial", "Course Reminder Set", $"A reminder for {course.Name} will be sent on {course.Start.AddDays(-1).ToString("MM/dd")}!", course.Id).ConfigureAwait(false);
                    await database.CreateNewNotification(course.Start, "start", "New Course starting soon!", $"{course.Name} starts on {course.Start.ToString("MM/dd")}!", course.Id).ConfigureAwait(false);
                    await database.CreateNewNotification(course.End, "end", "Your course is over today!", $"{course.Name} ends next today!", course.Id).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //canexecute to validate fields
        public bool CanSaveChanges()
        {

            var errorFound = false;

            if (Name is null || string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Instructor name cannot be blank.";
                OnPropertyChanged(nameof(NameError));
                errorFound = true;
            }

            if (Phone is null || string.IsNullOrWhiteSpace(Phone))
            {
                PhoneError = "Instructor phone cannot be blank.";
                OnPropertyChanged(nameof(PhoneError));
                errorFound = true;
            }

            if (Email is null || string.IsNullOrWhiteSpace(Email))
            {
                errorFound = true;
                EmailError = "Instructor email cannot be blank.";
                OnPropertyChanged(nameof(EmailError));
            }

            if (!errorFound)
            {
                NameError = "";
                EmailError = "";
                PhoneError = "";
            }

            return !errorFound;
        }

    }
}
