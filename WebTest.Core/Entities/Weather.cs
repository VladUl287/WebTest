using WebTest.Core.Attrubutes;

namespace WebTest.Core.Entities;

public sealed class Weather
{
    [NotMap]
    public Guid Id { get; init; }

    public DateOnly Date { get; init; }

    public TimeOnly Time { get; init; }

    public double Temperature { get; init; }

    public double RelativeHumidity { get; init; }

    public double DewPoint { get; init; }

    public double AtmospherePressure { get; init; }

    public string? WindDirection { get; init; }

    public double WindSpeed { get; init; }

    public double Cloudy { get; init; }

    public double CloudBase { get; init; }

    public string? HorizontalVisibility { get; init; }

    public string? WeatherConditions { get; init; }
}
