using WebTest.Core.Entities;

namespace WebTest.Models;

public sealed class WeatherList
{
    public Weather[] Weather {  get; init; } = Array.Empty<Weather>();

    public int PageCount { get; init; }
}
