using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoviesAPI;

public class MovieContextFactory : IDesignTimeDbContextFactory<MovieContext>
{
    public MovieContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MovieContext>();
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=moviesdb;Username=postgres;Password=postgres");

        return new MovieContext(optionsBuilder.Options);
    }
    
}