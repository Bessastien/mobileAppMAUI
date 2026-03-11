using SebastienDabertApp.Models;

namespace SebastienDabertApp.Data;

/// <summary>
/// Fournit les données mock des stations de ski.
/// Centralise les données en un seul endroit, séparé de la logique ViewModel.
/// </summary>
public static class MockResortDataProvider
{
    public static List<SkiResort> GetResorts() =>
    [
        new()
        {
            Id = 1,
            Name = "Chamonix Mont-Blanc",
            Description = "Station mythique au pied du Mont-Blanc, paradis du freeride et de l'alpinisme.",
            ImageUrl = "https://images.unsplash.com/photo-1522926193341-e9ffd686c60f?w=800",
            Latitude = 45.9237,
            Longitude = 6.8694
        },
        new()
        {
            Id = 2,
            Name = "Courchevel",
            Description = "Station prestigieuse des 3 Vallées, réputée pour ses pistes damées et son luxe.",
            ImageUrl = "https://images.unsplash.com/photo-1551524559-8af4e6624178?w=800",
            Latitude = 45.4154,
            Longitude = 6.6347
        },
        new()
        {
            Id = 3,
            Name = "Val Thorens",
            Description = "Plus haute station d'Europe (2 300 m), enneigement garanti et ambiance festive.",
            ImageUrl = "https://images.unsplash.com/photo-1520208422220-d12a3c588e6c?w=800",
            Latitude = 45.2980,
            Longitude = 6.5800
        },
        new()
        {
            Id = 4,
            Name = "Les Deux Alpes",
            Description = "Glacier skiable été comme hiver, spot légendaire du freestyle et du VTT.",
            ImageUrl = "https://images.unsplash.com/photo-1605540436563-5bca919ae766?w=800",
            Latitude = 45.0167,
            Longitude = 6.1167
        },
        new()
        {
            Id = 5,
            Name = "L'Alpe d'Huez",
            Description = "Station ensoleillée surnommée « l'île au soleil », célèbre pour sa piste de Sarenne.",
            ImageUrl = "https://images.unsplash.com/photo-1736088606573-a10abe4274cf?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            Latitude = 45.0911,
            Longitude = 6.0653
        },
        new()
        {
            Id = 6,
            Name = "Tignes",
            Description = "Station d'altitude reliée à Val d'Isère, ski sur glacier et ambiance sportive.",
            ImageUrl = "https://images.unsplash.com/photo-1565992441121-4367c2967103?w=800",
            Latitude = 45.4685,
            Longitude = 6.9068
        },
        new()
        {
            Id = 7,
            Name = "Méribel",
            Description = "Au cœur des 3 Vallées, station chaleureuse aux chalets en bois et forêts de sapins.",
            ImageUrl = "https://images.unsplash.com/photo-1551036359-aaa8fdf6314b?q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            Latitude = 45.3968,
            Longitude = 6.5657
        },
        new()
        {
            Id = 8,
            Name = "La Plagne",
            Description = "Domaine immense relié aux Arcs (Paradiski), idéal pour tous niveaux et familles.",
            ImageUrl = "https://images.unsplash.com/photo-1517483000871-1dbf64a6e1c6?w=800",
            Latitude = 45.5065,
            Longitude = 6.6771
        }
    ];
}
