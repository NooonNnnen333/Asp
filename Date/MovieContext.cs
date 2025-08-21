using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

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