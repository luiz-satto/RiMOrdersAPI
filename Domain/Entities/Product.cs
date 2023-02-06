using Domain.Primitives;

namespace Domain.Entities;

public sealed class Product: Entity
{
    internal Product()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    private Product(Guid id, string name, string description, double price, int stock)
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

    public static Product Create(Guid id, string name, string description, double price, int stock)
    {
        var product = new Product(id, name, description, price, stock);
        return product;
    }
}
