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
            ImageUrl = "chamonix.png",
            Latitude = 45.9237,
            Longitude = 6.8694
        },
        new()
        {
            Id = 2,
            Name = "Courchevel",
            Description = "Station prestigieuse des 3 Vallées, réputée pour ses pistes damées et son luxe.",
            ImageUrl = "courchevel.png",
            Latitude = 45.4154,
            Longitude = 6.6347
        },
        new()
        {
            Id = 3,
            Name = "Val Thorens",
            Description = "Plus haute station d'Europe (2 300 m), enneigement garanti et ambiance festive.",
            ImageUrl = "val_thorens.png",
            Latitude = 45.2980,
            Longitude = 6.5800
        },
        new()
        {
            Id = 4,
            Name = "Les Deux Alpes",
            Description = "Glacier skiable été comme hiver, spot légendaire du freestyle et du VTT.",
            ImageUrl = "les_deux_alpes.png",
            Latitude = 45.0167,
            Longitude = 6.1167
        },
        new()
        {
            Id = 5,
            Name = "L'Alpe d'Huez",
            Description = "Station ensoleillée surnommée « l'île au soleil », célèbre pour sa piste de Sarenne.",
            ImageUrl = "alpe_dhuez.png",
            Latitude = 45.0911,
            Longitude = 6.0653
        },
        new()
        {
            Id = 6,
            Name = "Tignes",
            Description = "Station d'altitude reliée à Val d'Isère, ski sur glacier et ambiance sportive.",
            ImageUrl = "tignes.png",
            Latitude = 45.4685,
            Longitude = 6.9068
        },
        new()
        {
            Id = 7,
            Name = "Méribel",
            Description = "Au cœur des 3 Vallées, station chaleureuse aux chalets en bois et forêts de sapins.",
            ImageUrl = "meribel.png",
            Latitude = 45.3968,
            Longitude = 6.5657
        },
        new()
        {
            Id = 8,
            Name = "La Plagne",
            Description = "Domaine immense relié aux Arcs (Paradiski), idéal pour tous niveaux et familles.",
            ImageUrl = "la_plagne.png",
            Latitude = 45.5065,
            Longitude = 6.6771
        }
    ];
}
