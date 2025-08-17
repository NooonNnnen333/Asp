using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieContext(DbContextOptions<MovieContext> options) : DbContext
{
    public DbSet<Move> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action"   },
            new Genre { Id = 2, Name = "Sci-Fi"   },
            new Genre { Id = 3, Name = "Fantasy"  },
            new Genre { Id = 4, Name = "Horror"   },
            new Genre { Id = 5, Name = "Romance"  }
        );

        modelBuilder.Entity<Move>().HasData(
            new Move { Id = 1, Name = "Inception",       GenereId = 1, Price = 12.99M, RealisDate = new DateOnly(2003, 1, 1) },
            new Move { Id = 2, Name = "The Dark Knight", GenereId = 1, Price = 14.99M, RealisDate = new DateOnly(2005, 1, 1) },
            new Move { Id = 3, Name = "Interstellar",    GenereId = 2, Price = 10.99M, RealisDate = new DateOnly(2007, 1, 1) },
            new Move { Id = 4, Name = "The Matrix",      GenereId = 2, Price =  9.99M, RealisDate = new DateOnly(1999, 1, 1) },
            new Move { Id = 5, Name = "The Lord of the Rings: The Fellowship of the Ring",
                       GenereId = 3, Price = 11.99M, RealisDate = new DateOnly(2001, 1, 1) },
            new Move { Id = 6, Name = "The Lord of the Rings: The Two Towers",
                       GenereId = 3, Price = 11.99M, RealisDate = new DateOnly(2002, 1, 1) }
        );
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=moviesdb;Username=postgres;Password=yourpassword");
        }
    }

    
    
} 