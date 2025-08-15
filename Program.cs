using Microsoft.EntityFrameworkCore;
using MoviesAPI;
using Npgsql;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieDbCS"))); 

var app = builder.Build();

/* Асинхронное добавление данных */
using (var scope = app.Services.CreateScope())
{
    var servieces = scope.ServiceProvider;

    try
    {
        var context = servieces.GetRequiredService<MovieContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка {ex.Message}");
    }
    
}



app.MapGet("/", () => "Hello World!");

app.Run();