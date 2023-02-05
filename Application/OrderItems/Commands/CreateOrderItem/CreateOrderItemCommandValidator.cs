using FluentValidation;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}