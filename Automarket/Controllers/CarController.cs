using Automarket.DAL.Interfaces;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.CarService.Handlers.DeleteCar;
using Automarket.Service.CarService.Handlers.GetAllCars;
using Automarket.Service.CarService.Handlers.GetCar;
using Automarket.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;
    private readonly IMediator _mediator;

    public CarController(ICarService carService, IMediator mediator)
    {
        _carService = carService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCars()
    {
        var request = new GetAllCars();
        
        var response = await _mediator.Send(request);
        
        if (response.StatusCode == Domain.Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }
        
        return RedirectToAction("Error");
    }

    [HttpGet]
    public async Task<IActionResult> GetCar(int id)
    {
        var request = new GetCar(id);
        
        var response = await _mediator.Send(request);

        if (response.StatusCode == Domain.Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var request = new DeleteCar(id);
        
        var response = await _mediator.Send(request);

        if (response.StatusCode == Domain.Enums.StatusCode.Ok)
        {
            return RedirectToAction("GetCars");
        }

        return RedirectToAction("Error");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save(int id)
    {
        if (id == 0)
        {
            return View();
        }

        var response = await _carService.GetCar(id);
        if (response.StatusCode == Domain.Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error");
    }

    [HttpPost]
    public async Task<IActionResult> Save(CarViewModel carViewModel)
    {
        if (ModelState.IsValid)
        {
            if (carViewModel.Id == 0)
            {
                await _carService.CreateCar(carViewModel);
            }
            else
            {
                await _carService.Edit(carViewModel.Id, carViewModel);
            }
        }

        return RedirectToAction("GetCars");
    }

    public IActionResult Error(string desc)
    {
        return View(desc);
    }
}