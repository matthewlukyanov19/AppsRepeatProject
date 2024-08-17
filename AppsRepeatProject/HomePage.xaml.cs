using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            // Navigate to the MainPage
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnGoToSettingsClicked(object sender, EventArgs e)
        {
            // Navigate to the SettingsPage
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
