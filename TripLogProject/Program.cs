using Microsoft.EntityFrameworkCore;
using TripLog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TripLogContext>(options =>
    options.UseInMemoryDatabase("TripLogDB"));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trips}/{action=Index}/{id?}");

app.Run();
