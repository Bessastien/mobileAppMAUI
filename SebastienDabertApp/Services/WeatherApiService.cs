using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SebastienDabertApp.Services;

public class WeatherApiService
{
    private readonly HttpClient _httpClient;

    public WeatherApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Interroge l'API Open-Meteo pour obtenir la température actuelle
    /// aux coordonnées données.
    /// </summary>
    public async Task<string> GetCurrentTemperatureAsync(double latitude, double longitude)
    {
        try
        {
            var lat = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var lon = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

            var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

            if (response?.CurrentWeather != null)
            {
                return $"{response.CurrentWeather.Temperature}°C";
            }

            return "N/A";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[WeatherApiService] Erreur: {ex.Message}");
            return "Erreur";
        }
    }
}

/// <summary>
/// Modèle de désérialisation de la réponse Open-Meteo.
/// </summary>
public class OpenMeteoResponse
{
    [JsonPropertyName("current_weather")]
    public CurrentWeather? CurrentWeather { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }
}

