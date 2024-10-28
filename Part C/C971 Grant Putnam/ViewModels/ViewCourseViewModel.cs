using C971_Grant_Putnam.Models;
using C971_Grant_Putnam.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.ViewModels
{
    [INotifyPropertyChanged]
    [QueryProperty("SelectedCourse", "SelectedCourse")]
    public partial class ViewCourseViewModel
    {

        [ObservableProperty]
        private Course selectedCourse;

        [ObservableProperty]
        private int carouselPosition;

        [ObservableProperty]
        private Action<int> carouselAction;

        [RelayCommand]
        async Task GoToEditCourse(Course course)
        {
            await Shell.Current.GoToAsync($"{nameof(AddEditCourse)}", true, 
                new Dictionary<string, object> {
                    {"SelectedCourse", course},
                });
        }

        [RelayCommand]
        async Task SwitchPosition(string position)
        {
            int index = Int32.Parse(position);

            CarouselPosition = index;

            CarouselAction(index);
        }

    }
}
