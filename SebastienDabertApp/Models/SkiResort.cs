using CommunityToolkit.Mvvm.ComponentModel;

namespace SebastienDabertApp.Models;

public partial class SkiResort : ObservableObject
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    [ObservableProperty]
    private string _currentTemperature = "Chargement...";
}



