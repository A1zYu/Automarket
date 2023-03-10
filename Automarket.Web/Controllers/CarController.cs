using System.Security.Cryptography;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Web.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;


    public CarController(ICarService service)
    {
        _carService = service;
    }
    // GET
    public async Task<IActionResult> GetCars()
    {
        var response = await _carService.GetCars();
        if (response.StatusCode==Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return RedirectToAction("GetCars");
    }

    [HttpGet]
    public async Task<IActionResult> GetCar(int id)
    {
        var response = await _carService.GetCar(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _carService.DeleteCar(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetCars");
        }

        return RedirectToAction("Error");
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        if (id == 0) return View();
        var response = await _carService.GetCar(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save(CarViewModel carViewModel)
    {
        if (ModelState.IsValid)
        {
            if (carViewModel.Id == 0)
            {
                await _carService.CreateCar(carViewModel);
            }
            else
            {
                await _carService.Edit(carViewModel.Id,carViewModel);
            }
        }

        return RedirectToAction("GetCars");
    }
}