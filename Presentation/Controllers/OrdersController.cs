using Application.OrderItems.Queries.GetOrderItemById;
using Application.Orders.Commands.CancelOrder;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.UpdateOrder;
using Application.Orders.Queries;
using Application.Orders.Queries.GetAllOrders;
using Domain.Entities;
using Domain.Shared;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Represents the orders controler
/// </summary>
public sealed class OrdersController : ApiController
{
    /// <summary>
    /// Gets the order with the specified identifier, if it exists.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The order with the specified identifier, if it exists.</returns>
    [HttpGet("Get:{orderId:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid orderId, CancellationToken cancellationToken)
    {
        var orderItemsQuery = new GetOrderItemsByIdQuery(orderId);
        var orderItemResponse = await Sender.Send(orderItemsQuery, cancellationToken);
        if (orderItemResponse is null)
        {
            throw new Exception(Error.NullValue);
        }

        var _orderItems = orderItemResponse.Select(item => OrderItem
            .Create(
                item.Id,
                orderId,
                item.ProductId,
                item.ProductName,
                item.ProductDescription,
                item.ProductPrice,
                item.ProductStock,
                item.Quantity))
            .ToList();

        var orderQuery = new GetOrderByIdQuery(orderId);
        var orderResponse = await Sender.Send(orderQuery, cancellationToken);
        if (orderResponse is null)
        {
            throw new Exception(Error.NullValue);
        }

        var _order = Order
            .Create(
                orderResponse.Id,
                orderResponse.Email,
                orderResponse.DeliveryAddress,
                orderResponse.DateCancelled,
                orderResponse.CreationDate,
                _orderItems);

        return Ok(_order);
    }

    /// <summary>
    /// Gets all orders with the specified pagination.
    /// </summary>
    /// <param name="PageNumber">The current page number.</param>
    /// <param name="RowsOfPage">The rows of the current page.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns all orders with the specified pagination.</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOrders(int PageNumber, int RowsOfPage, CancellationToken cancellationToken)
    {
        var ordersQuery = new GetAllOrdersQuery(PageNumber, RowsOfPage);
        var ordersResponse = await Sender.Send(ordersQuery, cancellationToken);
        if (ordersResponse is null)
        {
            throw new Exception(Error.NullValue);
        }

        var orders = new List<Order>();
        foreach (var order in ordersResponse)
        {
            var _order = (await GetOrder(order.Id, cancellationToken)) as ObjectResult;
            if (_order is not null)
            {
                var result = _order.Value as Order;
                if (result is not null)
                {
                    orders.Add(result);
                }
            }
        }

        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order based on the specified request.
    /// </summary>
    /// <param name="request">The create order request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created order.</returns>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateOrderCommand>();
        var orderId = await Sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOrder), new { orderId }, orderId);
    }

    /// <summary>
    /// Updates an order based on the specified request.
    /// </summary>
    /// <param name="request">The update order request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated order.</returns>
    [HttpPut("Update")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(
        [FromBody] UpdateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateOrderCommand>();
        var order = await Sender.Send(command, cancellationToken);
        return Ok(order);
    }

    /// <summary>
    /// Cancels an order based on the specified request.
    /// </summary>
    /// <param name="request">The cancel order request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>If the order was cancelled successfully.</returns>
    [HttpPost("Cancel")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelOrder(
        [FromBody] CancelOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CancelOrderCommand>();
        var cancelled = await Sender.Send(command, cancellationToken);
        return Ok(cancelled);
    }

    /// <summary>
    /// Deletes an order based on the specified request.
    /// </summary>
    /// <param name="request">The delete order request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>If the order was deleted successfully.</returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(
        [FromBody] DeleteOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<DeleteOrderCommand>();
        var deleted = await Sender.Send(command, cancellationToken);
        return Ok(deleted);
    }
}
