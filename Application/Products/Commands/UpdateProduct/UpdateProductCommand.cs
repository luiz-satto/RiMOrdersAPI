using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    double Price,
    int Stock
) : ICommand<Product>;