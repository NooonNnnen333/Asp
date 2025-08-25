using Microsoft.EntityFrameworkCore;
using MoviesAPI;

var builder = WebApplication.CreateBuilder(args);

// Временная проверка: что реально читается из конфигов
Console.WriteLine("CS = " + builder.Configuration.GetConnectionString("MovieDbCS"));

// Регистрация EF Core + Npgsql
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieDbCS")));

var app = builder.Build();

// Применяем миграции на старте (ОК оставлять для dev)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var db = services.GetRequiredService<MovieContext>();
        await db.Database.MigrateAsync();   // или db.Database.Migrate();
        Console.WriteLine("✅ Миграции применены");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Ошибка миграции: {ex.Message}");
    }
}

app.MapGet("/", () => "Hello World!");
app.MapMoviesEndpoint();



app.Run();