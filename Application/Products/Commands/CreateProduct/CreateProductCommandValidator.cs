using FluentValidation;

namespace Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Stock).NotEmpty();
    }
}
