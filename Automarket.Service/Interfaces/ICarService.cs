using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels.Car;

namespace Automarket.Service.Interfaces;

public interface ICarService
{
    Task<IBaseResponse<Car>> GetCar(int id);

    Task<IBaseResponse<bool>> CreateCar(CarViewModel carViewModel);
        
    Task<IBaseResponse<bool>> DeleteCar(int id);
    
    Task<IBaseResponse<Car>> GetCarByName(string name);
    
    Task<IBaseResponse<IEnumerable<Car>>> GetAllCars();

    Task<IBaseResponse<Car>> Edit(int id, CarViewModel model);
}