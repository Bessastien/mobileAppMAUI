using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SebastienDabertApp.Models;
using SebastienDabertApp.Services;

namespace SebastienDabertApp.ViewModels;

public partial class AddResortViewModel(ResortsViewModel resortsViewModel, WeatherApiService weatherService)
    : ObservableObject
{
    // ── Formulaire ────────────────────────────────────────────────────────────

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddResortCommand))]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddResortCommand))]
    private string _description = string.Empty;

    // ── Image (galerie) ───────────────────────────────────────────────────────

    private string _localImagePath = string.Empty;
    public string LocalImagePath
    {
        get => _localImagePath;
        set
        {
            if (SetProperty(ref _localImagePath, value))
            {
                OnPropertyChanged(nameof(HasImage));
                AddResortCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool HasImage => !string.IsNullOrWhiteSpace(LocalImagePath);


    // ── Localisation ──────────────────────────────────────────────────────────

    /// <summary>True = mode GPS | False = saisie manuelle</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsManualMode))]
    [NotifyCanExecuteChangedFor(nameof(AddResortCommand))]
    private bool _isGpsMode = true;

    public bool IsManualMode => !IsGpsMode;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddResortCommand))]
    private string _latitudeText = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddResortCommand))]
    private string _longitudeText = string.Empty;

    [ObservableProperty]
    private bool _isLocating;

    [ObservableProperty]
    private string _locationStatus = string.Empty;

    [ObservableProperty]
    private string _temperaturePreview = string.Empty;

    [ObservableProperty]
    private bool _isPreviewingTemp;

    // ── Feedback ──────────────────────────────────────────────────────────────

    [ObservableProperty]
    private bool _isSuccess;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    // ── Constructeur ──────────────────────────────────────────────────────────

    // ── Commandes de mode ─────────────────────────────────────────────────────

    [RelayCommand]
    private void SelectGpsMode()
    {
        IsGpsMode          = true;
        LatitudeText       = string.Empty;
        LongitudeText      = string.Empty;
        LocationStatus     = string.Empty;
        TemperaturePreview = string.Empty;
    }

    [RelayCommand]
    private void SelectManualMode()
    {
        IsGpsMode          = false;
        LocationStatus     = string.Empty;
        TemperaturePreview = string.Empty;
    }

    // ── Picker image ──────────────────────────────────────────────────────────

    [RelayCommand]
    private async Task PickImageAsync()
    {
        ErrorMessage = string.Empty;
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Choisir une photo",
                FileTypes   = FilePickerFileType.Images
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result is null) return;

            // Copie dans le dossier privé de l'app pour un accès persistant
            var destFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
            await using (var source = await result.OpenReadAsync())
            await using (var dest   = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                await source.CopyToAsync(dest);
            }

            LocalImagePath = destFile;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur : {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"[PickImage] {ex}");
        }
    }

    // ── Recherche image Unsplash ──────────────────────────────────────────────

    [RelayCommand]
    private async Task FetchImageFromApiAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Veuillez d'abord saisir le nom de la station";
            return;
        }

        ErrorMessage = string.Empty;
        IsLocating   = true;

        try
        {
            // NOTE: La clé API est en dur pour ce projet scolaire. 
            // Dans une vraie application, elle devrait être stockée de manière sécurisée (ex: Azure Key Vault) ou dans la configuration serveur.
            const string apiKey = "hlYtx2MUKbHJ9E2brL-YaDNYX3P_8_D_2cbArE8k8FQ";
            var query = Uri.EscapeDataString(Name.Trim());
            var url   = $"https://api.unsplash.com/search/photos?query={query}&per_page=1&client_id={apiKey}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var results = doc.RootElement.GetProperty("results");
            if (results.GetArrayLength() == 0)
            {
                ErrorMessage = "Aucune image trouvée pour cette station.";
                return;
            }

            var imageUrl = results[0]
                .GetProperty("urls")
                .GetProperty("regular")
                .GetString();

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                ErrorMessage = "URL d'image invalide retournée par l'API.";
                return;
            }

            LocalImagePath = imageUrl;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Erreur réseau : {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"[FetchImageFromApi] {ex}");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur lors de la recherche d'image : {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"[FetchImageFromApi] {ex}");
        }
        finally
        {
            IsLocating = false;
        }
    }

    // ── GPS ───────────────────────────────────────────────────────────────────

    [RelayCommand]
    private async Task UseMyLocationAsync()
    {
        IsLocating         = true;
        LocationStatus     = "Localisation en cours...";
        TemperaturePreview = string.Empty;
        ErrorMessage       = string.Empty;

        try
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                LocationStatus = "Permission de localisation refusee.";
                return;
            }

            var location = await Geolocation.Default.GetLocationAsync(
                new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)));

            if (location is null)
            {
                LocationStatus = "Position introuvable. Reessayez.";
                return;
            }

            LatitudeText   = location.Latitude.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
            LongitudeText  = location.Longitude.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
            LocationStatus = $"Position obtenue : {LatitudeText} N, {LongitudeText} E";

            await FetchTemperaturePreviewAsync(location.Latitude, location.Longitude);
        }
        catch (FeatureNotSupportedException)
        {
            LocationStatus = "GPS non disponible sur cet appareil.";
        }
        catch (Exception ex)
        {
            LocationStatus = $"Erreur : {ex.Message}";
        }
        finally
        {
            IsLocating = false;
        }
    }

    // ── Prévisualisation manuelle ─────────────────────────────────────────────

    [RelayCommand]
    private async Task PreviewTemperatureAsync()
    {
        if (!TryParseCoordinates(out var lat, out var lon))
        {
            ErrorMessage = "Coordonnées invalides. Ex : 45.9237 et 6.8694";
            return;
        }
        ErrorMessage   = string.Empty;
        LocationStatus = $"{lat:F4} N, {lon:F4} E";
        await FetchTemperaturePreviewAsync(lat, lon);
    }

    private async Task FetchTemperaturePreviewAsync(double lat, double lon)
    {
        IsPreviewingTemp   = true;
        TemperaturePreview = "Chargement…";
        try   { TemperaturePreview = await weatherService.GetCurrentTemperatureAsync(lat, lon); }
        catch { TemperaturePreview = "N/A"; }
        finally { IsPreviewingTemp = false; }
    }

    // ── Ajout ─────────────────────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanAddResort))]
    private async Task AddResortAsync()
    {
        ErrorMessage = string.Empty;
        IsSuccess    = false;

        double latitude = 0, longitude = 0;

        if (IsManualMode && !TryParseCoordinates(out latitude, out longitude))
        {
            ErrorMessage = "Coordonnées invalides. Vérifiez la latitude et la longitude.";
            return;
        }

        if (IsGpsMode && !string.IsNullOrWhiteSpace(LatitudeText))
            TryParseCoordinates(out latitude, out longitude);

        var newId = resortsViewModel.Resorts.Count > 0
            ? resortsViewModel.Resorts.Max(r => r.Id) + 1
            : 1;

        resortsViewModel.Resorts.Add(new SkiResort
        {
            Id          = newId,
            Name        = Name.Trim(),
            Description = Description.Trim(),
            ImageUrl    = string.IsNullOrWhiteSpace(LocalImagePath) ? "ski_mountain.jpeg" : LocalImagePath,
            Latitude    = latitude,
            Longitude   = longitude,
            CurrentTemperature = string.IsNullOrWhiteSpace(TemperaturePreview) ? "N/A" : TemperaturePreview
        });

        IsSuccess = true;

        Name = Description = LocalImagePath = LatitudeText = LongitudeText
             = LocationStatus = TemperaturePreview = string.Empty;
        IsGpsMode = true;

        await Task.Delay(800);
        await Shell.Current.GoToAsync("..");
    }

    private bool CanAddResort()
        => !string.IsNullOrWhiteSpace(Name)
        && !string.IsNullOrWhiteSpace(Description)
        && HasImage
        && !string.IsNullOrWhiteSpace(LatitudeText)
        && !string.IsNullOrWhiteSpace(LongitudeText);

    private bool TryParseCoordinates(out double latitude, out double longitude)
    {
        var culture = System.Globalization.CultureInfo.InvariantCulture;
        return double.TryParse(LatitudeText.Replace(',', '.'),
                   System.Globalization.NumberStyles.Float, culture, out latitude)
             & double.TryParse(LongitudeText.Replace(',', '.'),
                   System.Globalization.NumberStyles.Float, culture, out longitude);
    }
}






