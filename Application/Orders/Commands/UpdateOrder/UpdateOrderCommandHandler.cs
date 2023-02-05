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
        var updatedOrder = _orderRepository.Update(new(
            request.OrderId,
            request.Email,
            request.DeliveryAddress,
            request.CreationDate,
            request.DateCancelled));

        if (updatedOrder is null)
        {
            throw new Exception(Error.NullValue);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return updatedOrder;
    }
}
