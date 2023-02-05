using FluentValidation;

namespace Application.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}
