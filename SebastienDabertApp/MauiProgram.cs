using Microsoft.Extensions.Logging;
using SebastienDabertApp.Services;
using SebastienDabertApp.ViewModels;

namespace SebastienDabertApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // HttpClient : Singleton pour réutiliser les connexions HTTP
        builder.Services.AddSingleton<HttpClient>();

        // Services : Singleton car sans état mutable, une seule instance suffit
        builder.Services.AddSingleton<WeatherApiService>();

        // ViewModels : Transient pour rafraîchir les données à chaque visite
        builder.Services.AddTransient<ResortsViewModel>();

        // Pages : Transient pour recréer la page (et son ViewModel) à chaque navigation
        builder.Services.AddTransient<ResortsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}