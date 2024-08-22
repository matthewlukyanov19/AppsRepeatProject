namespace AppsRepeatProject;
using System.Collections.ObjectModel;

public partial class HistoryPage : ContentPage
{
    private GameHistory gameHistoryManager;

    public HistoryPage(GameHistory gameHistoryManager)
    {
        InitializeComponent();
        this.gameHistoryManager = gameHistoryManager;
        BindingContext = gameHistoryManager;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
