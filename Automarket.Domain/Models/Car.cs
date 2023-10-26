using Automarket.Domain.Enums;

namespace Automarket.Domain.Models;

public class Car : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public string Model { get; set; }

    public CarType Type { get; set; }

    public decimal Price { get; set; }

    public DateTime CreateDate { get; set; }
}