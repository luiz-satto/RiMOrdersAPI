using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid orderId)
        : base($"The order with the identifier {orderId} was not found.")
    {

    }
}
