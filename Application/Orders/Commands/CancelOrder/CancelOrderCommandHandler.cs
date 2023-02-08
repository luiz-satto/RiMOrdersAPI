using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.Orders.Commands.CancelOrder;

internal sealed class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _orderRepository.Get(request.OrderId).Value;
        if (order is null)
        {
            return false;
        }

        order.DateCancelled = request.DateCancelled;
        order.IsCancelled = true;

        var updatedOrder = _orderRepository.Update(order);
        if (updatedOrder is null)
        {
            throw new Exception(Error.NullValue);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}