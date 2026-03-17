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
    private readonly DatabaseService _databaseService;

    public ObservableCollection<SkiResort> Resorts { get; } = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private SkiResort? _selectedResort;

    public ResortsViewModel(WeatherApiService weatherService, DatabaseService databaseService)
    {
        _weatherService = weatherService;
        _databaseService = databaseService; 
        LoadResortsAsync();
    }

    /// <summary>
    /// Charge la liste des stations de ski depuis la base de données.
    /// </summary>
    private async void LoadResortsAsync()
    {
        if (IsLoading) return;
        IsLoading = true;

        try 
        {
            var resorts = await _databaseService.GetResortsAsync();
            
            // On s'assure de modifier la collection sur le thread UI
            MainThread.BeginInvokeOnMainThread(() => 
            {
                Resorts.Clear();
                foreach (var resort in resorts)
                {
                    Resorts.Add(resort);
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur chargement: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Commande déclenchée lors de la sélection d'une station dans la CollectionView.
    /// Navigue vers la page de détail en passant la station comme paramètre.
    /// </summary>
    [RelayCommand]
    private async Task GoToDetailAsync(SkiResort? resort)
    {
        if (resort is null) return;

        // Réinitialise la sélection pour permettre de re-sélectionner la même station
        SelectedResort = null;

        await Shell.Current.GoToAsync(nameof(ResortDetailPage), new Dictionary<string, object>
        {
            { "SelectedResort", resort }
        });
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
