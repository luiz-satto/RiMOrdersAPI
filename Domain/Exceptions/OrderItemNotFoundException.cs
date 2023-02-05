using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class OrderItemNotFoundException : NotFoundException
{
    public OrderItemNotFoundException(Guid orderId)
        : base($"The items from the order with the identifier {orderId} was not found.")
    {

    }
}
