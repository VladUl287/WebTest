using Microsoft.EntityFrameworkCore;
using WebTest.Core.Contracts.Repositories;
using WebTest.Core.Contracts.Services;
using WebTest.Core.Services;
using WebTest.Infrastructure.Database;
using WebTest.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    builder.Services.AddScoped<IWeatherService, WeatherService>();
    builder.Services.AddScoped<IWeatherManager, WeatherManager>();
    builder.Services.AddScoped<IWeatherPresenter, WeatherPresenter>();

    builder.Services.AddDbContext<DatabaseContext>(
            options =>
            {
                options.UseInMemoryDatabase("db");
            }
        );
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Archive}/{action=Index}");
}
app.Run();
