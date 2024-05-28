using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options) // inherit from dbcontext class
{
    public DbSet<Game> Games => Set<Game>();  // set object properties to Game

    public DbSet<Genre> Genres => Set<Genre>(); // set object properties to Genre

    protected override void OnModelCreating(ModelBuilder modelBuilder) // overriding method to predefine genres SeedGenres when migrations builds db
    // only use this for static db data that doesn't need to be updated by user interactions
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fun" },
            new { Id = 2, Name = "Roleplay" },
            new { Id = 3, Name = "Sports" },
            new { Id = 5, Name = "Adventure" }
            );
    }


}
