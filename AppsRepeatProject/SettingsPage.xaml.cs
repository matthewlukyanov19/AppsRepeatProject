namespace AppsRepeatProject
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            // Toggle between dark and light mode
            if (e.Value)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
        }

        private void OnTimerLengthChanged(object sender, ValueChangedEventArgs e)
        {
            // Update the timer length label
            TimerLengthLabel.Text = $"{(int)e.NewValue} seconds";
        }
    }
}



