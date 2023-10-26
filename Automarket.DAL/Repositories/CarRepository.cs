using Automarket.DAL.Interfaces;
using Automarket.Domain.Enums;
using Automarket.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Create(Car model)
    {
        await _context.AddAsync(model);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<Car?> Get(int id)
    {
        return await _context.cars.FirstOrDefaultAsync(car => car.Id == id);
    }

    public async Task<List<Car>> Select()
    {
        return await _context.cars.ToListAsync();
    }

    public async Task<bool> Delete(Car model)
    {
        _context.Remove(model);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Car> Update(Car model)
    {
        _context.cars.Update(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<Car?> GetByName(string name)
    {
        return await _context.cars.FirstOrDefaultAsync(car => car.Name == name);
    }
}