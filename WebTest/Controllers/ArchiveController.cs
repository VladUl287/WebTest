using Microsoft.AspNetCore.Mvc;
using WebTest.Core.Contracts.Repositories;
using WebTest.Core.Contracts.Services;
using WebTest.Core.Dtos;
using WebTest.Models;

namespace WebTest.Controllers;

public sealed class ArchiveController : Controller
{
    private readonly IWeatherService weatherService;
    private readonly IWeatherPresenter weatherPresenter;

    public ArchiveController(IWeatherService weatherService, IWeatherPresenter weatherPresenter)
    {
        this.weatherService = weatherService;
        this.weatherPresenter = weatherPresenter;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] WeathreFilters filters)
    {
        var weather = await weatherPresenter.GetAll(filters);
        var count = await weatherPresenter.GetCount();
        return View(new WeatherList
        {
            PageCount = count / filters.Size,
            Weather = weather,
        });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateArchives());
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormFile[] archives)
    {
        await weatherService.Create(archives);
        return View(new CreateArchives());
    }
}
