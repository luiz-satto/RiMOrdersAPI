namespace Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductRequest(
    Guid ProductId,
    string Name,
    string Description,
    double Price,
    int Stock
);