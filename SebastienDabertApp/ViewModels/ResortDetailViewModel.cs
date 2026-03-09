using CommunityToolkit.Mvvm.ComponentModel;
using SebastienDabertApp.Models;

namespace SebastienDabertApp.ViewModels;

/// <summary>
/// ViewModel de la page de détail d'une station de ski.
/// Reçoit la station sélectionnée via le paramètre de navigation Shell "SelectedResort".
/// </summary>
[QueryProperty(nameof(SelectedResort), "SelectedResort")]
public partial class ResortDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private SkiResort? _selectedResort;
}

