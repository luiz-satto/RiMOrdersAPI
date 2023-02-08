using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.Orders.Commands.UpdateOrder;

internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _orderRepository.Get(request.OrderId).Value;
        if (order is null)
        {
            throw new Exception(Error.NullValue);
        }

        if (request.DateCancelled is not null) 
        {
            order = Order.Create(
                request.OrderId,
                order.Email,
                order.DeliveryAddress,
                request.DateCancelled);
        }
        else
        {
            if (request.Email is not null) { order.Email = request.Email; }
            if (request.DeliveryAddress is not null) { order.DeliveryAddress = request.DeliveryAddress; }
            if (request.Items is not null) { order.Items = request.Items; }
        }

        var result = _orderRepository.Update(order);
        if (result is null)
        {
            throw new Exception(Error.NullValue);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}
