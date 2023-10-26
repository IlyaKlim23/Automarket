using Automarket.DAL.Interfaces;
using Automarket.Domain.Enums;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using MediatR;

namespace Automarket.Service.CarService.Handlers.GetCar;

public class GetCarHandler: IRequestHandler<GetCar, IBaseResponse<Car>>
{
    private readonly ICarRepository _carRepository;

    public GetCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IBaseResponse<Car>> Handle(GetCar request, CancellationToken cancellationToken)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.Get(request.Id);
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
                Description = $"[GetCar]: {e.Message}"
            };
        }
    }
}