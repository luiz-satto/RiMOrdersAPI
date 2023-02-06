using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.OrderItems.Commands.CreateOrderItem;

internal sealed class CreateOrderItemCommandHandler : ICommandHandler<CreateOrderItemCommand, bool>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
    {
        _orderItemRepository = orderItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = OrderItem.Create(
            Guid.NewGuid(),
            request.OrderId,
            request.OrderEmail,
            request.OrderDeliveryAddress,
            request.DateCancelled,
            request.ProductId,
            request.ProductName,
            request.ProductDescription,
            request.ProductPrice,
            request.ProductStock,
            request.Quantity);

        _orderItemRepository.Insert(orderItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return orderItem is not null;
    }
}