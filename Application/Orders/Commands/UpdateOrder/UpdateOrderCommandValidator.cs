using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().Matches(new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));
        RuleFor(x => x.DeliveryAddress).NotEmpty();
        RuleFor(x => x.CreationDate).NotEmpty();
    }
}
