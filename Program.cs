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

// Create database and seed data - with comprehensive error handling
try
{
    Console.WriteLine("Starting database initialization...");
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        Console.WriteLine("Database context created successfully");
        
        // Delete and recreate database in Development mode to apply seed data changes
        if (app.Environment.IsDevelopment())
        {
            try
            {
                Console.WriteLine("Attempting to delete database (Development mode)...");
                db.Database.EnsureDeleted();
                Console.WriteLine("Database deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not delete database: {ex.GetType().Name} - {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
                }
            }
        }
        
        Console.WriteLine("Attempting to create database...");
        db.Database.EnsureCreated();
        Console.WriteLine("Database created successfully");
        
        // Verify database was created
        var canConnect = db.Database.CanConnect();
        Console.WriteLine($"Database can connect: {canConnect}");
        
        if (canConnect)
        {
            var productCount = db.Products.Count();
            Console.WriteLine($"Products in database: {productCount}");
        }
    }
    Console.WriteLine("Database initialization completed successfully");
}
catch (Exception ex)
{
    Console.WriteLine($"CRITICAL ERROR initializing database:");
    Console.WriteLine($"Exception Type: {ex.GetType().FullName}");
    Console.WriteLine($"Message: {ex.Message ?? "null"}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner Exception Type: {ex.InnerException.GetType().FullName}");
        Console.WriteLine($"Inner Message: {ex.InnerException.Message ?? "null"}");
    }
    try
    {
        Console.WriteLine($"Stack trace: {ex.StackTrace ?? "null"}");
    }
    catch
    {
        Console.WriteLine("Could not print stack trace");
    }
    // Continue anyway - app might work without database
    Console.WriteLine("Continuing application startup despite database error...");
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
