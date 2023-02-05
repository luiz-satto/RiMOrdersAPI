using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updatedProduct = _productRepository.Update(new(
            request.ProductId,
            request.Name,
            request.Description,
            request.Price,
            request.Stock));

        if (updatedProduct is null)
        {
            throw new Exception(Error.NullValue);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return updatedProduct;
    }
}
