using Microsoft.EntityFrameworkCore;
using zomage.Data;
using System.IO;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=zomage.db"));

var app = builder.Build();

// Create database - simplified and safer approach
// Run asynchronously to not block app startup
_ = Task.Run(async () =>
{
    await Task.Delay(1000); // Wait 1 second for app to start
    try
    {
        Console.WriteLine("Starting database initialization...");
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
            
            // Create database
            try
            {
                db.Database.EnsureCreated();
                Console.WriteLine("Database created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database creation error: {ex.GetType().Name} - {ex.Message}");
                // Continue without database
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database init error: {ex.GetType().Name} - {ex.Message}");
        // Continue - app will work without database
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
