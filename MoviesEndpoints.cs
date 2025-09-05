using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;

namespace MoviesAPI;

public static class MoviesEndpoints
{
    public static RouteGroupBuilder MapMoviesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("movies");

        group.MapGet("/",
            async (MovieContext movieContext) => await movieContext.Movies.Include("Genre").ToListAsync());
        return group;

        group.MapGet("/{id}",
            async (MovieContext movieContext, int id) =>
            {

                Move? movie = await movieContext.Movies.Include("Genre").FirstOrDefaultAsync(x => x.Id == id);
                return movie is null ? Results.NotFound() : Results.Ok(movie);

            }
        );

        return group;
    }
    
    
}