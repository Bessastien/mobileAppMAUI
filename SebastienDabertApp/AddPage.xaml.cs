using SebastienDabertApp.ViewModels;

namespace SebastienDabertApp;

public partial class AddPage : ContentPage
{
    public AddPage(AddResortViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
