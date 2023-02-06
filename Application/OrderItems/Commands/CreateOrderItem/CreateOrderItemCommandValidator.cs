using FluentValidation;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(x => x.Order).NotEmpty();
        RuleFor(x => x.Product).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}