using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        

    }
}
