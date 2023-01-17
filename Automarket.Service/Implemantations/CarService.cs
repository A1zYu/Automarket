using System.Collections;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels.Car;
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

    public async Task<IBaseResponse<Car>> GetCar(int id)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.Get(id);
            if (car == null)
            {
                baseResponse.Description = "User not found";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                return baseResponse;
            }

            baseResponse.Data = car;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCar]: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<Car>> GetByName(string name)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.GetByName(name);
            if (car == null)
            {
                baseResponse.Description = "User not found";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                return baseResponse;
            }

            baseResponse.Data = car;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetByName]: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
    {
        var baseResponse = new BaseResponse<CarViewModel>();
        try
        {
            var car = new Car()
            {
                Name = carViewModel.Name,
                Description = carViewModel.Description,
                DateCreate = DateTime.Now,
                Model = carViewModel.Model,
                Speed = carViewModel.Speed,
                Price = carViewModel.Price,
                TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)
            };
            await _carRepository.Create(car);
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<CarViewModel>()
            {
                Description = $"[CreateCar]: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<bool>> DeleteCar(int id)
    {
        var baseResponse = new BaseResponse<bool>()
        {
            Data = true
        };
        try
        {
            var car =await _carRepository.Get(id);
            if (car==null)
            {
                baseResponse.Description = "User not found";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                return baseResponse;
            }

            await _carRepository.Delete(car);
            return baseResponse;

        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteCar]: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}