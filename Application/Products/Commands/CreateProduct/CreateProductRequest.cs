namespace Application.Products.Commands.CreateProduct;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    double Price,
    int Stock
);
