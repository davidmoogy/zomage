using Microsoft.EntityFrameworkCore;
using zomage.Data;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Database
// Use /tmp directory on Railway for writable database location
var dbPath = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? 
             (Environment.GetEnvironmentVariable("HOME") != null 
                ? Path.Combine(Environment.GetEnvironmentVariable("HOME")!, "zomage.db")
                : "zomage.db");
                
Console.WriteLine($"Database path: {dbPath}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

// Create database and seed data
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        // Delete and recreate database in Development mode to apply seed data changes
        if (app.Environment.IsDevelopment())
        {
            try
            {
                db.Database.EnsureDeleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not delete database: {ex.Message}");
            }
        }
        
        db.Database.EnsureCreated();
        Console.WriteLine("Database initialized successfully");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing database: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    // Don't crash the app, continue without database
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Disable HTTPS redirection on Railway (they handle it at proxy level)
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure port for hosting platforms (Railway, Render, etc.)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Clear();
    app.Urls.Add($"http://0.0.0.0:{port}");
}
else
{
    // Default ports for local development
    app.Urls.Add("http://localhost:5000");
    app.Urls.Add("https://localhost:5001");
}

app.Run();
