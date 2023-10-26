using Automarket.DAL.Interfaces;
using Automarket.Domain.Enums;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using MediatR;

namespace Automarket.Service.CarService.Handlers.GetAllCars;

public class GetAllCarsHandler: IRequestHandler<GetAllCars, BaseResponse<IEnumerable<Car>>>
{
    private readonly ICarRepository _carRepository;

    public GetAllCarsHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<BaseResponse<IEnumerable<Car>>> Handle(GetAllCars request, CancellationToken cancellationToken)
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
}