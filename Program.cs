using Microsoft.EntityFrameworkCore;
using zomage.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Database
var dbPath = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? "zomage.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

// Create database and seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Delete and recreate database in Development mode to apply seed data changes
    if (app.Environment.IsDevelopment())
    {
        db.Database.EnsureDeleted();
    }
    
    db.Database.EnsureCreated();
}

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
