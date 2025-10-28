using Asp.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Asp;

public class MovieContext(DbContextOptions<MovieContext> options) : DbContext(options)
{
    // Также допустим другой вариант иницилизации
    // public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
    
    public DbSet<movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Nacizm" },
            new Genre { Id = 2, Name = "Fascizm" },
            new Genre {Id = 3, Name = "Socialism"}
            );
        
        
        modelBuilder.Entity<movie>().HasData(
            new movie
            {
                Id = 14,
                Name = "Cewrv",
                Price = 999,
                GenereId = 1,
                RealisDate = new DateOnly(1990, 5, 20)
            },
            
        new movie
            {
                Id = 9,
                Name = "MyCampf",
                Price = 999,
                GenereId = 1,
                RealisDate = new DateOnly(1991, 5, 20)
            },
            
        new movie
            {
                Id = 15,
                Name = "Film",
                Price = 999,
                GenereId = 1,
                RealisDate = new DateOnly(2001, 5, 20)
            }
        );

    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=moviesdb;Username=postgres;Password=postgres");
        }
    }
    
    
}