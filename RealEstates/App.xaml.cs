using RealEstates.Pages;

namespace RealEstates
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new RegisterPage();
        }
    }
}
