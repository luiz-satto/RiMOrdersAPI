using FluentValidation;

namespace Application.OrderItems.Commands.UpdateOrderItem;

public sealed class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderItemId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}