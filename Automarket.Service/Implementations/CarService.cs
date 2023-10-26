using Automarket.DAL.Interfaces;
using Automarket.Domain.Enums;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Interfaces;

namespace Automarket.Service.Implementations;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IBaseResponse<bool>> CreateCar(CarViewModel carViewModel)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var car = new Car
            {
                Description = carViewModel.Description,
                CreateDate = DateTime.Now,
                Name = carViewModel.Name,
                Model = carViewModel.Model,
                Type = (CarType)Convert.ToInt32(carViewModel.Type),
                Price = carViewModel.Price
            };

            await _carRepository.Create(car);

            baseResponse.Data = true;
            baseResponse.StatusCode = StatusCode.Ok;

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>
            {
                Description = $"[CreateCar]: {e.Message}"
            };
        }
    }


    public async Task<IBaseResponse<Car>> GetCarByName(string name)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.GetByName(name);
            if (car == null)
            {
                baseResponse.Description = "Элемент не найден";
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }

            baseResponse.Data = car;
            baseResponse.StatusCode = StatusCode.Ok;

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Car>
            {
                Description = $"[GetCarByName]: {e.Message}"
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
                baseResponse.Description = "Элемент не найден";
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }

            baseResponse.Data = car;
            baseResponse.StatusCode = StatusCode.Ok;

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCar]: {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteCar(int id)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var car = await _carRepository.Get(id);
            if (car == null)
            {
                baseResponse.Description = "Элемент не найден";
                baseResponse.StatusCode = StatusCode.Ok;
                baseResponse.Data = false;

                return baseResponse;
            }

            baseResponse.Data = true;

            await _carRepository.Delete(car);

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteCar]: {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Car>>> GetAllCars()
    {
        var baseResponse = new BaseResponse<IEnumerable<Car>>();
        try
        {
            var cars = await _carRepository.Select();
            if (cars.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;
            }

            baseResponse.Data = cars;
            baseResponse.StatusCode = StatusCode.Ok;

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Car>>()
            {
                Description = $"[GetAllCars]: {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel model)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.Get(id);

            if (car == null)
            {
                baseResponse.Description = "Элемент не найден";
                baseResponse.StatusCode = StatusCode.Ok;

                return baseResponse;
            }

            car.Description = model.Description;
            car.CreateDate = model.CreateDate;
            car.Name = model.Name;
            car.Model = model.Model;
            car.Price = model.Price;

            await _carRepository.Update(car);

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[Edit]: {e.Message}"
            };
        }
    }
}