using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class Homes : ContentPage
    {
        public Homes()
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


