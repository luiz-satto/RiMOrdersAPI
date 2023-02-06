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
        if (orderItem is null)
        {
            throw new Exception(Error.NullValue);
        }

        orderItem.Quantity = request.Quantity;
        var result = _orderItemRepository.Update(orderItem);
        if (result is null)
        {
            throw new Exception(Error.NullValue);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}