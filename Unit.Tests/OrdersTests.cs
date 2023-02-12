using Application.Orders.Queries;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Unit.Tests;

public sealed class OrdersTests : TestBase
{
    [Fact]
    public async Task GetOrderById_API_Should_ReturnExpectedResponse_1()
    {
        // Arrange.
        var orderId = "2a5e24e2-1af2-45ca-b556-75774b88919d";
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new OrderResponse(
            Guid.Parse(orderId),
            "jucajarbas@gmail.com",
            "45,CASTLEKNOCK MEADOWS,LAUREL LODGE,DUBLIN 15,D15A973",
            DateTime.Parse("2023-02-05T17:15:37.63"),
            DateTime.Parse("2023-02-05T17:15:37.63"),
            0);

        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await SendAsync(HttpMethod.Get, $"/api/Orders/Get:{orderId}");

        // Assert.
        await AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    public async Task GetOrderById_API_Should_ReturnExpectedResponse_2()
    {
        // Arrange.
        var orderId = "2de4cf4d-a161-4819-a53a-9f7edf711f43";
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new OrderResponse(
            Guid.Parse(orderId),
            "aaaaa@email.com",
            "Qewqewqe,32131",
            DateTime.Parse("2023-02-08T21:07:47.9304496"),
            null,
            0);

        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await SendAsync(HttpMethod.Get, $"/api/Orders/Get:{orderId}");

        // Assert.
        await AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
}