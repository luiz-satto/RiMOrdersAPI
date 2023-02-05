using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Orders.Commands.CreateOrder;

internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(Guid.NewGuid(), request.Email, request.DeliveryAddress, request.CreationDate, null);
        _orderRepository.Insert(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
