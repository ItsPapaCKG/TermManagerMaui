using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views;

public partial class EditAssessments : ContentPage
{
	public EditAssessments(AddEditAssessmentViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    private void DatePicker_ObjStart(object sender, DateChangedEventArgs e)
    {
        var vm = BindingContext as AddEditAssessmentViewModel;

        if (vm != null)
        {
            vm.ObjectiveAssessmentStart = e.NewDate;
            vm.SaveAssessmentsCommand.NotifyCanExecuteChanged();
        }
    }

    private void DatePicker_ObjEnd(object sender, DateChangedEventArgs e)
    {
        var vm = BindingContext as AddEditAssessmentViewModel;

        if (vm != null)
        {
            vm.ObjectiveAssessmentEnd = e.NewDate;
            vm.SaveAssessmentsCommand.NotifyCanExecuteChanged();
        }
    }

    private void DatePicker_PerStart(object sender, DateChangedEventArgs e)
    {
        var vm = BindingContext as AddEditAssessmentViewModel;

        if (vm != null)
        {
            vm.PerformanceAssessmentStart = e.NewDate;
            vm.SaveAssessmentsCommand.NotifyCanExecuteChanged();
        }
    }

    private void DatePicker_PerEnd(object sender, DateChangedEventArgs e)
    {
        var vm = BindingContext as AddEditAssessmentViewModel;

        if (vm != null)
        {
            vm.PerformanceAssessmentEnd = e.NewDate;
            vm.SaveAssessmentsCommand.NotifyCanExecuteChanged();
        }
    }
}