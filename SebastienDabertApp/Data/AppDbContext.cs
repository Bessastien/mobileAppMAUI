using Microsoft.EntityFrameworkCore;
using SebastienDabertApp.Models;

namespace SebastienDabertApp.Data;

public partial class AppDbContext : DbContext
{
    public DbSet<SkiResort> Resorts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Crée la base de données si elle n'existe pas
        // Dans une vraie application, on utiliserait les migrations.
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Initialisation de la base de données avec les données Mock
        modelBuilder.Entity<SkiResort>().HasData(MockResortDataProvider.GetResorts());
    }
}
