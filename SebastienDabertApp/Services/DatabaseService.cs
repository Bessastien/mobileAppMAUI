using Microsoft.EntityFrameworkCore;
using SebastienDabertApp.Data;
using SebastienDabertApp.Models;

namespace SebastienDabertApp.Services;

public class DatabaseService(AppDbContext context)
{
    public async Task<List<SkiResort>> GetResortsAsync()
    {
        // Les données mock sont maintenant insérées automatiquement à la création de la base 
        // grâce au Data Seeding dans AppDbContext.OnModelCreating
        return await context.Resorts.ToListAsync();
    }

    public async Task AddResortAsync(SkiResort resort)
    {
        // On s'assure que l'Id est 0 pour l'auto-incrément
        resort.Id = 0; 
        await context.Resorts.AddAsync(resort);
        await context.SaveChangesAsync();
    }
}
