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

        // ViewModels
        // ResortsViewModel en Singleton : sa collection Resorts est partagée avec AddResortViewModel
        builder.Services.AddSingleton<ResortsViewModel>();
        builder.Services.AddTransient<ResortDetailViewModel>();
        builder.Services.AddTransient<AddResortViewModel>();
        builder.Services.AddSingleton<ToolsViewModel>();

        // Pages : Transient pour recréer la page à chaque navigation
        builder.Services.AddTransient<ResortsPage>();
        builder.Services.AddTransient<ResortDetailPage>();
        builder.Services.AddTransient<AddPage>();
        builder.Services.AddSingleton<ToolsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}