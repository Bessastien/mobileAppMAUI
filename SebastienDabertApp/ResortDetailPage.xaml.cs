using SebastienDabertApp.ViewModels;

namespace SebastienDabertApp;

public partial class ResortDetailPage : ContentPage
{
    public ResortDetailPage(ResortDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

