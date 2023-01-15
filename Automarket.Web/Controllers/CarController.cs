using System.Security.Cryptography;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Web.Controllers;

public class CarController : Controller
{
    private readonly ICarService _service;


    public CarController(ICarService service)
    {
        _service = service;
    }
    // GET
    public async Task<IActionResult> GetCars()
    {
        var response = await _service.GetCars();
        if (response.StatusCode==Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return RedirectToAction("GetCars");
    }
}