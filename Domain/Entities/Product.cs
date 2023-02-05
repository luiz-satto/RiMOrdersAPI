using Domain.Primitives;

namespace Domain.Entities;

public sealed class Product: Entity
{
    public Product(Guid id, string name, string description, double price, int stock)
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public int Stock { get; private set; }
}
