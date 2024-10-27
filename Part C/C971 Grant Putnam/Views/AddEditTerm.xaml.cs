using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views;

public partial class AddEditTerm : ContentPage
{
	public AddEditTerm(AddEditTermViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		var vm = BindingContext as AddEditTermViewModel;

		if (vm != null)
		{
			vm.SelectedTermName = e.NewTextValue;
		}
    }
}