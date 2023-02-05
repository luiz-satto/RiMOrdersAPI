using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.OrderItems.Commands.DeleteOrderItem;

internal sealed class DeleteOrderItemCommandHandler : ICommandHandler<DeleteOrderItemCommand, bool>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
    {
        _orderItemRepository = orderItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItemDeleted = _orderItemRepository.Delete(request.OrderItemId);
        if (!orderItemDeleted)
        {
            throw new Exception(new Error("Error.OrderCouldNotBeDeleted", $"The order:{request.OrderItemId} could not be deleted."));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return orderItemDeleted;
    }
}