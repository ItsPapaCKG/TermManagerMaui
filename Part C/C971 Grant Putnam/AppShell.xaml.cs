using C971_Grant_Putnam.ViewModels;
using C971_Grant_Putnam.Views;

namespace C971_Grant_Putnam
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddEditTerm), typeof(AddEditTerm));
        }
    }
}
