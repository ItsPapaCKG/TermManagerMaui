using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }

}
