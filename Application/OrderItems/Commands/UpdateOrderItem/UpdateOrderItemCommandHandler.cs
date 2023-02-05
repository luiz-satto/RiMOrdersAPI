using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.OrderItems.Commands.UpdateOrderItem;

internal sealed class UpdateOrderItemCommandHandler : ICommandHandler<UpdateOrderItemCommand, OrderItem>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
    {
        _orderItemRepository = orderItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderItem> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = _orderItemRepository.Get(request.OrderItemId).Value;
        orderItem.Quantity = request.Quantity;

        var updatedOrderItem = _orderItemRepository.Update(orderItem);
        if (updatedOrderItem is null)
        {
            throw new Exception(Error.NullValue);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return updatedOrderItem;
    }
}