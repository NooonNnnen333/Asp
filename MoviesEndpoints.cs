using Microsoft.EntityFrameworkCore;

namespace MoviesAPI;

public static class MoviesEndpoints
{
    public static RouteGroupBuilder MapMoviesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("movies");

        group.MapGet("/",
            async (MovieContext movieContext) => await movieContext.Genres.Include("Name").ToListAsync());
        return group;
    }
    
    
}