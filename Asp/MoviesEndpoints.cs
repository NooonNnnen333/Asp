using Microsoft.EntityFrameworkCore;
using Asp.Entities;

namespace Asp;

public static class MoviesEndpoints
{
    public static RouteGroupBuilder MapMoviesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("movies").WithParameterValidation();

        group.MapGet("/",
            async (MovieContext movieContext) => await movieContext.Movies.Include("Genre").ToListAsync()); // "Получение" всех данных фильмов

        group.MapGet("/{id}",
            async (MovieContext movieContext, int id) =>
            {
                movie? movie = await movieContext.Movies.Include("Genre").FirstOrDefaultAsync(x => x.Id == id);
                return movie is null ? Results.NotFound() : Results.Ok(movie);
            }); // "Получение" данных объекта, у которого некий id

        group.MapPost("/", async (movie newMovie, MovieContext movieContext) =>
            {
                newMovie.Genre = await movieContext.Genres.FirstOrDefaultAsync(x => x.Id == newMovie.GenereId);
                movieContext.Movies.Add(newMovie);
                await movieContext.SaveChangesAsync();
                return Results.Created($"/movies/{newMovie.Id}", newMovie);
            }); // Добавление нового объекта

        group.MapPut("/{id}", async (int id, movie updateMovie, MovieContext movieContext) =>
            {
                movie? movie = await movieContext.Movies.FindAsync(id);
                if (movie == null)
                {
                    return Results.NotFound();
                }

                //movieContext.Entry(movie).CurrentValues.SetValues(updateMove);
                if (updateMovie.Name is not null) { movie.Name = updateMovie.Name; }
                if (updateMovie.GenereId != 0) {movie.GenereId = updateMovie.GenereId; movie.Genre = movieContext.Genres.Find(updateMovie.GenereId);}
                if (updateMovie.Price != 0){movie.Price = updateMovie.Price; }
                if (updateMovie.RealisDate != default){movie.RealisDate = updateMovie.RealisDate; }

                movieContext.Movies.Update(movie);
                await movieContext.SaveChangesAsync();
                return Results.NoContent();
                
            }); // Частичное обновление данных 

        group.MapDelete("/{id}", async (int id, MovieContext movieContext) =>
            {
            movie? movie_ = await movieContext.Movies.FindAsync(id);
            if (movie_ is null)
            {
                return Results.NotFound();
            }

            movieContext.Movies.Remove(movie_);
            await movieContext.SaveChangesAsync();

            return Results.NoContent();
        }); // Удаление
        
        return group;
    }
    
    
}