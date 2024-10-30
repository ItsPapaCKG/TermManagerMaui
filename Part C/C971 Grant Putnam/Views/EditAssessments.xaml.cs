using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views;

public partial class EditAssessments : ContentPage
{
	public EditAssessments(AddEditAssessmentViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}