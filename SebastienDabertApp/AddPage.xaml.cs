using SebastienDabertApp.ViewModels;

namespace SebastienDabertApp;

public partial class AddPage : ContentPage
{
    public AddPage(AddResortViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnNameEntryTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (BindingContext is AddResortViewModel vm)
        {
            vm.Name = e.NewTextValue ?? string.Empty;
        }
    }
}
