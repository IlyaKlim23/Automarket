using Automarket.Domain.Models;
using Automarket.Domain.Response;
using MediatR;

namespace Automarket.Service.CarService.Handlers.GetAllCars;

public class GetAllCars: IRequest<BaseResponse<IEnumerable<Car>>>
{
    
}