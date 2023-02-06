using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.OrderEmail)
            .NotEmpty()
            .Matches(new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));

        RuleFor(x => x.OrderDeliveryAddress)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ProductDescription)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Quantity)
            .NotEmpty();
    }
}