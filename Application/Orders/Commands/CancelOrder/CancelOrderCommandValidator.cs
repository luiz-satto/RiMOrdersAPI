using FluentValidation;

namespace Application.Orders.Commands.CancelOrder;

public sealed class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(x => x.DateCancelled).NotEmpty();
    }
}
