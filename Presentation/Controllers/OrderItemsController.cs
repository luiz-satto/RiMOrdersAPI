﻿using Application.OrderItems.Commands.CreateOrderItem;
using Application.OrderItems.Commands.DeleteOrderItem;
using Application.OrderItems.Commands.UpdateOrderItem;
using Application.OrderItems.Dtos;
using Application.OrderItems.Queries.GetOrderItemById;
using Domain.Entities;
using Domain.Shared;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public sealed class OrderItemsController : ApiController
{
    /// <summary>
    /// Gets the order items with the specified identifier, if it exists.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The order items with the specified identifier, if it exists.</returns>
    [HttpGet("Get:{orderId:guid}")]
    [ProducesResponseType(typeof(OrderItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderItems(Guid orderId, CancellationToken cancellationToken)
    {
        var query = new GetOrderItemsByIdQuery(orderId);
        var orderItemResponse = await Sender.Send(query, cancellationToken);
        if (orderItemResponse is null)
        {
            throw new Exception(Error.NullValue);
        }

        var orderItems = new List<OrderItemDto>();
        foreach (var item in orderItemResponse)
        {
            var product = new Product(item.ProductId, item.ProductName, item.ProductDescription, item.ProductPrice, item.ProductStock);
            var orderItem = new OrderItemDto(item.Id, item.OrderId, item.Quantity, product);
            orderItems.Add(orderItem);
        }

        return Ok(orderItems);
    }

    /// <summary>
    /// Creates a new order item based on the specified request.
    /// </summary>
    /// <param name="request">The create order item request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>If the order item was created.</returns>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrderItem(
        [FromBody] CreateOrderItemRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateOrderItemCommand>();
        var created = await Sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOrderItems), new { request.OrderId }, created);
    }

    /// <summary>
    /// Updates an order item based on the specified request.
    /// </summary>
    /// <param name="request">The update order item request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated order item.</returns>
    [HttpPut("Update")]
    [ProducesResponseType(typeof(OrderItem), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderItem(
        [FromBody] UpdateOrderItemRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateOrderItemCommand>();
        var orderItem = await Sender.Send(command, cancellationToken);
        if (orderItem is null)
        {
            throw new Exception(Error.NullValue);
        }

        return Ok(orderItem);
    }

    /// <summary>
    /// Deletes an order item based on the specified request.
    /// </summary>
    /// <param name="request">The delete order item request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>If the order item was deleted successfully.</returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderItem(
        [FromBody] DeleteOrderItemRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<DeleteOrderItemCommand>();
        var deleted = await Sender.Send(command, cancellationToken);
        return Ok(deleted);
    }
}