using Automarket.Domain.Response;
using MediatR;

namespace Automarket.Service.CarService.Handlers.DeleteCar;

public class DeleteCar: IRequest<IBaseResponse<bool>>
{
    public int Id { get; }

    public DeleteCar(int id)
    {
        Id = id;
    }
}