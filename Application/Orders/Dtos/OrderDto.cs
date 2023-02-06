using Application.OrderItems.Dtos;
using Application.OrderItems.Queries.GetOrderItemById;
using Application.Orders.Queries;

namespace Application.Orders.Dtos;

public sealed class OrderDto
{
    private OrderDto(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime creationDate,
        DateTime? dateCancelled,
        List<OrderItemDto> orderItems)
    {
        Id = id;
        Email = email;
        DeliveryAddress = deliveryAddress;
        CreationDate = creationDate;
        DateCancelled = dateCancelled;
        IsCancelled = dateCancelled is not null;
        OrderItems = orderItems;
    }

    public Guid Id { get; }
    public string Email { get; }
    public string DeliveryAddress { get; }
    public DateTime CreationDate { get; }
    public DateTime? DateCancelled { get; }
    public bool IsCancelled { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; }

    public static OrderDto Create(OrderResponse orderResponse, IEnumerable<OrderItemResponse> orderItemResponse)
    {
        var orderItems = new List<OrderItemDto>();
        foreach (var item in orderItemResponse)
        {
            var orderItem = OrderItemDto.Create(item);
            orderItems.Add(orderItem);
        }

        var order = new OrderDto(
            orderResponse.Id,
            orderResponse.Email,
            orderResponse.DeliveryAddress,
            orderResponse.CreationDate,
            orderResponse.DateCancelled,
            orderItems);

        return order;
    }
}
