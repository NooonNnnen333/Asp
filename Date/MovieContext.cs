using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Nacizm" },
            new Genre { Id = 2, Name = "Fascizm" },
            new Genre {Id = 3, Name = "Socialism"}
            );
        

    }
    */
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=moviesdb;Username=postgres;Password=postgres");
        }
    }

    public DbSet<Move> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
}