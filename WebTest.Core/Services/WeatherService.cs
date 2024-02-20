using Microsoft.AspNetCore.Http;
using WebTest.Core.Contracts.Repositories;
using WebTest.Core.Contracts.Services;
using WebTest.Core.Entities;
using WebTest.Mapping;

namespace WebTest.Core.Services;

public sealed class WeatherService : IWeatherService
{
    private readonly IWeatherManager weatherManager;

    public WeatherService(IWeatherManager weatherManager)
    {
        this.weatherManager = weatherManager;
    }

    public async Task Create(IFormFile[] archives)
    {
        for (int i = 0; i < archives.Length; i++)
        {
            try
            {
                var file = archives[i];

                using var fileStream = new MemoryStream();
                await file.CopyToAsync(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);

                var weather = fileStream.MapExcelToObject<Weather>();

                await weatherManager.Create(weather.ToArray());
            }
            catch
            {
                //logging
            }
        }
    }
}
