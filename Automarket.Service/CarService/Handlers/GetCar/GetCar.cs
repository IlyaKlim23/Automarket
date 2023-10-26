using Automarket.Domain.Models;
using Automarket.Domain.Response;
using MediatR;

namespace Automarket.Service.CarService.Handlers.GetCar;

public class GetCar: IRequest<IBaseResponse<Car>>
{
    public int Id { get; }

    public GetCar(int id)
    {
        Id = id;
    }
}