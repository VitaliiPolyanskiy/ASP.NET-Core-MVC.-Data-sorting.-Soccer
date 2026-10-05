using Microsoft.EntityFrameworkCore;
using Soccer.Models;

var builder = WebApplication.CreateBuilder(args);

// Отримуємо рядок підключення з файлу конфігурації
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Додаємо контекст SoccerContext як сервіс у програму
builder.Services.AddDbContext<SoccerContext>(options => options.UseSqlServer(connection));

// Додаємо сервіси MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); // Обробляє запити до файлів у папці wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teams}/{action=Index}/{id?}");

app.Run();