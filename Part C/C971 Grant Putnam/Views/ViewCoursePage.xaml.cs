using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views;

public partial class ViewCoursePage : ContentPage
{
	public ViewCoursePage(ViewCourseViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}