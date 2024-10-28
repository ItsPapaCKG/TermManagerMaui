using C971_Grant_Putnam.ViewModels;

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
}