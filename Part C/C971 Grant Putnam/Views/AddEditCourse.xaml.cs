using C971_Grant_Putnam.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace C971_Grant_Putnam.Views;

public partial class AddEditCourse : ContentPage
{
	public AddEditCourse(AddEditCourseViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var vm = BindingContext as AddEditCourseViewModel;

        if (vm != null)
        {
            vm.CourseEdit.Instructor_Name = e.NewTextValue;
        }
    }

    private void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
    {
        var vm = BindingContext as AddEditCourseViewModel;

        if (vm != null)
        {
            vm.CourseEdit.Instructor_Phone = e.NewTextValue;
        }
    }

    private void Entry_TextChanged_2(object sender, TextChangedEventArgs e)
    {
        var vm = BindingContext as AddEditCourseViewModel;

        if (vm != null)
        {
            vm.CourseEdit.Instructor_Email = e.NewTextValue;
        }
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        var vm = BindingContext as AddEditCourseViewModel;

        if (vm != null)
        {
            vm.Start = e.NewDate;
            vm.SaveChangesToCourseCommand.NotifyCanExecuteChanged();
        }
    }

    private void DatePicker_DateSelected_1(object sender, DateChangedEventArgs e)
    {
        var vm = BindingContext as AddEditCourseViewModel;

        if (vm != null)
        {
            vm.End = e.NewDate;
            vm.SaveChangesToCourseCommand.NotifyCanExecuteChanged();
        }
    }
}