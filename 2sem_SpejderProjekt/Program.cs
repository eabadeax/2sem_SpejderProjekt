
using _2sem_SpejderProjekt.Data;
using Microsoft.EntityFrameworkCore;
using _2sem_SpejderProjekt.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Gør Razor Pages tilgængelige
builder.Services.AddRazorPages();
builder.Services.AddScoped<ActivityRepository>();

// Forbind til databasen
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
