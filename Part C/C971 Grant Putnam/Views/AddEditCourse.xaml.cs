using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views;

public partial class AddEditCourse : ContentPage
{
	public AddEditCourse(AddEditCourseViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}