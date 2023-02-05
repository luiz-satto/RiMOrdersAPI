using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.Products.Commands.DeleteProduct;

internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productDeleted = _productRepository.Delete(request.ProductId);
        if (!productDeleted)
        {
            throw new Exception(new Error("Error.ProductCouldNotBeDeleted", $"The product:{request.ProductId} could not be deleted."));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return productDeleted;
    }
}