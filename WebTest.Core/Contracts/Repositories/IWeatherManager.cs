using WebTest.Core.Entities;

namespace WebTest.Core.Contracts.Repositories;

public interface IWeatherManager
{
    Task Create(Weather[] weather);
}
