using FluentValidation;

namespace Application.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
