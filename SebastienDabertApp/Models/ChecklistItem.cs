using CommunityToolkit.Mvvm.ComponentModel;

namespace SebastienDabertApp.Models;

public class ChecklistItem : ObservableObject
{
    public string Label { get; set; } = string.Empty;

    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set => SetProperty(ref _isChecked, value);
    }
}


