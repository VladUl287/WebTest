using Microsoft.EntityFrameworkCore;
using WebTest.Core.Contracts.Repositories;
using WebTest.Core.Dtos;
using WebTest.Core.Entities;
using WebTest.Infrastructure.Database;
using WebTest.Infrastructure.Exteinsions;

namespace WebTest.Infrastructure.Repository;

public sealed class WeatherPresenter : IWeatherPresenter
{
    private readonly DatabaseContext databaseContext;

    public WeatherPresenter(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public Task<Weather[]> GetAll(WeathreFilters filters)
    {
        return databaseContext.Weather
            .AsNoTracking()
            .SetFilters(filters)
            .ToArrayAsync();
    }

    public Task<int> GetCount()
    {
        return databaseContext.Weather.CountAsync();
    }
}
