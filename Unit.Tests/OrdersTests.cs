using Application.OrderItems.Commands.CreateOrderItem;
using Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Unit.Tests;

public class OrdersTests
{
    private readonly OrdersController _ordersController;
    private readonly OrderItemsController _orderItemsController;

    public OrdersTests()
    {
        _ordersController = new OrdersController();
        _orderItemsController = new OrderItemsController();
    }

    [Fact]
    public void Orders_Controller_Instance_Should_Be_NotNull()
    {
        // Arrage        
        // Act
        // Assert
        Assert.NotNull(_ordersController);
    }

    [Fact]
    public async void CreateOrder_Returns_Order_Id()
    {
        // Arrage
        var request = new CreateOrderRequest(
            RandomString(10),
            RandomString(100),
            null);

        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        // Act
        var result = await _ordersController.CreateOrder(request, cts.Token);
        var objResult = result as ObjectResult;
        var guid = objResult is not null ? objResult.Value.ToString() : "";

        // Assert
        Assert.True(Guid.TryParse(guid, out Guid _guid));
    }

    [Fact]
    public void OrderItems_Controller_Instance_Should_Be_NotNull()
    {
        // Arrage        
        // Act
        // Assert
        Assert.NotNull(_orderItemsController);
    }

    private static readonly Random Random = new();
    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)])
            .ToArray()
        );
    }
}