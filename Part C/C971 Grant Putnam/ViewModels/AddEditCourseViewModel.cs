using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("SelectedCourse","SelectedCourse")]
    [QueryProperty("EditMode","EditMode")]
    public partial class AddEditCourseViewModel : IQueryAttributable
    {
        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private Course courseEdit;

        [ObservableProperty]
        private bool editMode;

        private DatabaseService database;

        public AddEditCourseViewModel(DatabaseService db)
        {
            database = db;

            if (CourseEdit == null)
            {
                CourseEdit = new Course();
            }
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

            }
            else 
            {
                CourseEdit = new Course();
            }

        }

        //TODO relay command to save
        [RelayCommand]
        public async Task SaveChangesToCourse(Course course)
        {
            try
            {
                if (course is null) { throw new Exception("New instance of course being edited is null."); }

                if (editMode && SelectedCourse is not null)
                {
                    await database.UpdateCourse(SelectedCourse.Id, course);

                    return;
                }
                else
                {
                    await database.AddCourse(course);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //canexecute to validate fields
    }
}
