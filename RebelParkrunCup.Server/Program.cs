using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;

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

// Add SQLite database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=parkrun.db"));

var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowAllOrigins");

// Seed test data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Ensures the database is created

    // Check if data already exists to prevent duplicate inserts
    if (!dbContext.Runners.Any())
    {
        dbContext.Runners.Add(new Runner
        {
            FirstName = "Nicholas",
            LastName = "Bannayi",
            BaselineTimeMins = 22,
            BaselineTimeSecs = 12
        });

        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();