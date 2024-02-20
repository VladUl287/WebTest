using WebTest.Core.Dtos;
using WebTest.Core.Entities;

namespace WebTest.Core.Contracts.Repositories;

public interface IWeatherPresenter
{
    Task<Weather[]> GetAll(WeathreFilters filters);

    Task<int> GetCount();
}
