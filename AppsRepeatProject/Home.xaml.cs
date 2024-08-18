using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnGoToSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
