using System.Diagnostics;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Automarket.Web.Models;

namespace Automarket.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICarRepository _carRepository;

    public HomeController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}