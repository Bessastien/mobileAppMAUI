using SebastienDabertApp.ViewModels;

namespace SebastienDabertApp;

public partial class ToolsPage : ContentPage
{
    private readonly ToolsViewModel _viewModel;

    public ToolsPage(ToolsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel     = viewModel;
        BindingContext = viewModel;
    }

    /// <summary>
    /// Relaye le changement d'état d'une CheckBox vers le ViewModel
    /// pour rafraîchir le compteur de progression.
    /// </summary>
    private void OnCheckBoxCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        _viewModel.OnItemCheckedChanged();
    }
}
