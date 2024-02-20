using WebTest.Core.Contracts.Repositories;
using WebTest.Core.Entities;
using WebTest.Infrastructure.Database;

namespace WebTest.Infrastructure.Repository;

public sealed class WeatherManager : IWeatherManager
{
    private readonly DatabaseContext databaseContext;

    public WeatherManager(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task Create(Weather[] weather)
    {
        await databaseContext.AddRangeAsync(weather);
        await databaseContext.SaveChangesAsync();
    }
}
