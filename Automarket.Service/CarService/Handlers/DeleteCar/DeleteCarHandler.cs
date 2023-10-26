using Automarket.DAL.Interfaces;
using Automarket.Domain.Enums;
using Automarket.Domain.Response;
using MediatR;

namespace Automarket.Service.CarService.Handlers.DeleteCar;

public class DeleteCarHandler: IRequestHandler<DeleteCar, IBaseResponse<bool>>
{
    private readonly ICarRepository _carRepository;

    public DeleteCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IBaseResponse<bool>> Handle(DeleteCar request, CancellationToken cancellationToken)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var car = await _carRepository.Get(request.Id);
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
}