using C971_Grant_Putnam.ViewModels;

namespace C971_Grant_Putnam.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Frame frame)
            {
                await MainThread.InvokeOnMainThreadAsync(async () => {
                    frame.BackgroundColor = Colors.Gray;

                    await Task.Delay(100);

                    frame.BackgroundColor = Colors.LightGray;
                });
                
            }
        }
    }

}
