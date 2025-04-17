using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;
using Microsoft.AspNetCore.Session;
using RebelParkrunCup.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient(); 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<GcsUploader>();
builder.Services.AddHostedService<GcsUploadOnShutdown>();

// Add SQLite database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=parkrun.db"));

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential for the app
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Optional, session timeout duration
});

var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowAllOrigins");

// This is crucial for serving Blazor WebAssembly (Client) files
app.UseBlazorFrameworkFiles(); // Serve Blazor WebAssembly files
app.UseStaticFiles(); // Serve other static files (like images, CSS)
app.MapFallbackToFile("index.html"); // Fallback to index.html when routing doesn't match

// Configure the HTTP request pipeline.
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

