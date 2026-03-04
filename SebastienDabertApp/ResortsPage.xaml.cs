using SebastienDabertApp.ViewModels;

namespace SebastienDabertApp;

public partial class ResortsPage : ContentPage
{
    private readonly ResortsViewModel _viewModel;

    public ResortsPage(ResortsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadTemperaturesCommand.ExecuteAsync(null);
    }
}

