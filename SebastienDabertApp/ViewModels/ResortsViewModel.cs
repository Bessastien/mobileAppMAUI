using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SebastienDabertApp.Data;
using SebastienDabertApp.Models;
using SebastienDabertApp.Services;

namespace SebastienDabertApp.ViewModels;

public partial class ResortsViewModel : ObservableObject
{
    private readonly WeatherApiService _weatherService;

    public ObservableCollection<SkiResort> Resorts { get; } = new();

    [ObservableProperty]
    private bool _isLoading;

    public ResortsViewModel(WeatherApiService weatherService)
    {
        _weatherService = weatherService;
        LoadResorts();
    }

    /// <summary>
    /// Charge la liste des stations de ski depuis le fournisseur de données mock.
    /// </summary>
    private void LoadResorts()
    {
        foreach (var resort in MockResortDataProvider.GetResorts())
        {
            Resorts.Add(resort);
        }
    }

    /// <summary>
    /// Commande appelée pour rafraîchir les températures de toutes les stations.
    /// </summary>
    [RelayCommand]
    private async Task LoadTemperaturesAsync()
    {
        IsLoading = true;

        try
        {
            var tasks = Resorts.Select(async resort =>
            {
                var temp = await _weatherService.GetCurrentTemperatureAsync(
                    resort.Latitude, resort.Longitude);
                resort.CurrentTemperature = temp;
            });

            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ResortsViewModel] Erreur: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}



