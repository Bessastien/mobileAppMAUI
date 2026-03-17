using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SebastienDabertApp.Data;
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

        // Base de données SQLite
        // On définit le chemin de la BDD dans le dossier local de l'application
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "ski_resorts.db");
        
        // On enregistre le DbContext. 
        // Note: Pour une application MAUI simple (mono-utilisateur), un DbContext Singleton est acceptable.
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"), ServiceLifetime.Singleton);

        // HttpClient : Singleton pour réutiliser les connexions HTTP
        builder.Services.AddSingleton<HttpClient>();

        // Services : Singleton, car sans état mutable, une seule instance suffit
        builder.Services.AddSingleton<WeatherApiService>();
        builder.Services.AddSingleton<DatabaseService>();

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