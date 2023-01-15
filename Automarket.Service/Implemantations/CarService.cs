using System.Collections;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Service.Interfaces;

namespace Automarket.Service.Implemantations;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
    {
        var baseResponse = new BaseResponse<IEnumerable<Car>>();
        try
        {
            var cars = await _carRepository.Select();
            if (cars.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.Data = cars;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Car>>()
            {
                Description = $"[GetCars]: {ex.Message}"
            };
        }
    }
}