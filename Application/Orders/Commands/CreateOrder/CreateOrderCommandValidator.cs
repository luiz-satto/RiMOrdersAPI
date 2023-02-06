using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.DeliveryAddress)
            .NotEmpty();

        RuleFor(x => x.CreationDate)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches(new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));
    }
}
