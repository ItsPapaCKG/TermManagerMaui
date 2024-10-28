using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views;

public partial class ViewCoursePage : ContentPage
{
	public ViewCoursePage(ViewCourseViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;

		vm.CarouselAction = MoveButtonTo;
		var d = new CourseDetails();
		d.BindingContext = vm;


		//Carousel.ItemsSource = new ContentView[] { d };
	}

	public async void MoveButtonTo(int column)
	{
		double newPosition = column * ButtonBackground.Width;
		await ButtonBackground.TranslateTo(newPosition,0,250,Easing.CubicInOut);
	}
}