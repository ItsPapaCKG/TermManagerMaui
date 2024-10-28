using C971_Grant_Putnam.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("SelectedCourse","SelectedCourse")]
    public partial class AddEditCourseViewModel : IQueryAttributable
    {
        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private Course courseEdit;

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

        }
    }
}
