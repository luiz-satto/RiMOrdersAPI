using FluentValidation;

namespace Application.OrderItems.Commands.DeleteOrderItem;

public sealed class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderItemId).NotEmpty();
    }
}