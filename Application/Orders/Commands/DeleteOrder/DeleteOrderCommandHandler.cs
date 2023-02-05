using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.Orders.Commands.DeleteOrder;

internal sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderDeleted = _orderRepository.Delete(request.OrderId);
        if (!orderDeleted)
        {
            throw new Exception(new Error("Error.OrderCouldNotBeDeleted", $"The order:{request.OrderId} could not be deleted."));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return orderDeleted;
    }
}
