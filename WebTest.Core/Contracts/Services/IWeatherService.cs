using Microsoft.AspNetCore.Http;
using WebTest.Core.Entities;

namespace WebTest.Core.Contracts.Services;

public interface IWeatherService
{
    Task Create(IFormFile[] archives);
}
