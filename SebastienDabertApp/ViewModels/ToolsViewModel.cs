using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SebastienDabertApp.Models;

namespace SebastienDabertApp.ViewModels;

public partial class ToolsViewModel : ObservableObject
{
    // ── Convertisseur d'altitude ──────────────────────────────────────────────

    private string _altitudeMetersText = string.Empty;
    public string AltitudeMetersText
    {
        get => _altitudeMetersText;
        set
        {
            if (SetProperty(ref _altitudeMetersText, value))
            {
                OnPropertyChanged(nameof(AltitudeFeet));
                OnPropertyChanged(nameof(HasAltitude));
            }
        }
    }

    /// <summary>Conversion instantanée mètres → pieds.</summary>
    public string AltitudeFeet =>
        double.TryParse(
            AltitudeMetersText.Replace(',', '.'),
            System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture,
            out var meters) ? $"{meters * 3.28084:F1} ft" : string.Empty;

    public bool HasAltitude => !string.IsNullOrWhiteSpace(AltitudeFeet);

    // ── Checklist de départ ───────────────────────────────────────────────────

    public ObservableCollection<ChecklistItem> Checklist { get; } = new()
    {
        new ChecklistItem { Label = "Skis / Snowboard" },
        new ChecklistItem { Label = "Chaussures de ski" },
        new ChecklistItem { Label = "Forfait remontees" },
        new ChecklistItem { Label = "Casque" },
        new ChecklistItem { Label = "Lunettes / Masque" },
        new ChecklistItem { Label = "Creme solaire" },
        new ChecklistItem { Label = "Veste et pantalon de ski" },
        new ChecklistItem { Label = "Gants" },
        new ChecklistItem { Label = "DVA" },
        new ChecklistItem { Label = "Pelle" },
        new ChecklistItem { Label = "Sonde" },
    };

    /// <summary>Nombre d'éléments cochés / total.</summary>
    public string ChecklistProgress =>
        $"{Checklist.Count(i => i.IsChecked)} / {Checklist.Count}";

    /// <summary>Remet tous les items à non-coché.</summary>
    [RelayCommand]
    private void ResetChecklist()
    {
        foreach (var item in Checklist)
            item.IsChecked = false;

        RefreshProgress();
    }

    /// <summary>Appelé depuis la vue quand une CheckBox change.</summary>
    public void OnItemCheckedChanged() => RefreshProgress();

    private void RefreshProgress() =>
        OnPropertyChanged(nameof(ChecklistProgress));
}
