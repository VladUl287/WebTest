using WebTest.Core.Dtos;
using WebTest.Core.Entities;

namespace WebTest.Infrastructure.Exteinsions;

internal static class Filters
{
    internal static IQueryable<Weather> SetFilters(this IQueryable<Weather> query, WeathreFilters? filters)
    {
        ArgumentNullException.ThrowIfNull(query);

        if (filters is not null)
        {
            var take = filters.Size;
            var skip = (filters.Page - 1) * filters.Size;
            query = query.Skip(skip).Take(take);
        }

        return query;
    }
}
