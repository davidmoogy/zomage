using Microsoft.EntityFrameworkCore;
using zomage.Data;
using System.IO;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Database
// Use writable directory on Railway
string dbPath;
var homeDir = Environment.GetEnvironmentVariable("HOME");
var dataDir = Environment.GetEnvironmentVariable("DATA_DIRECTORY");

if (!string.IsNullOrEmpty(dataDir))
{
    dbPath = Path.Combine(dataDir, "zomage.db");
}
else if (!string.IsNullOrEmpty(homeDir))
{
    dbPath = Path.Combine(homeDir, "zomage.db");
}
else
{
    // Fallback to current directory
    dbPath = "zomage.db";
}

Console.WriteLine($"Database path: {dbPath}");
Console.WriteLine($"HOME: {homeDir ?? "null"}");
Console.WriteLine($"DATA_DIRECTORY: {dataDir ?? "null"}");

try
{
    // Ensure directory exists
    var dbDir = Path.GetDirectoryName(dbPath);
    if (!string.IsNullOrEmpty(dbDir) && !Directory.Exists(dbDir))
    {
        Directory.CreateDirectory(dbDir);
        Console.WriteLine($"Created directory: {dbDir}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Warning: Could not create database directory: {ex.Message}");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

// Create database - simplified and safer approach
Console.WriteLine("Starting database initialization...");
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Database context created");
        
        // Only delete in development
        if (app.Environment.IsDevelopment())
        {
            try
            {
                db.Database.EnsureDeleted();
                Console.WriteLine("Old database deleted");
            }
            catch { /* Ignore delete errors */ }
        }
        
        // Create database without seed data first
        try
        {
            db.Database.EnsureCreated();
            Console.WriteLine("Database created");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database creation error: {ex.GetType().Name}");
            // Continue without database
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Database init error: {ex.GetType().Name}");
    // Continue - app will work without database
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
