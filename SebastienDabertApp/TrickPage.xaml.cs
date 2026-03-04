namespace SebastienDabertApp;

public partial class TrickPage : ContentPage
{
    public TrickPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}

