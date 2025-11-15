using Microsoft.EntityFrameworkCore;
using Asp;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Временная проверка: что реально читается из конфигов
Console.WriteLine("CS = " + builder.Configuration.GetConnectionString("MovieDbCS"));

// Регистрация EF Core + Npgsql
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieDbCS")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapMoviesEndpoint();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    var db = scope.ServiceProvider.GetRequiredService<MovieContext>();
    await db.Database.MigrateAsync();
    Console.WriteLine("✅ Миграции применены");
}


app.Run();