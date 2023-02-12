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
    public async Task GivenARequest_WhenCallingGetOrdersById_ThenTheAPIReturnsExpectedResponse()
    {
        // Arrange.
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new OrderResponse(
            Guid.Parse("2a5e24e2-1af2-45ca-b556-75774b88919d"),
            "jucajarbas@gmail.com",
            "45,CASTLEKNOCK MEADOWS,LAUREL LODGE,DUBLIN 15,D15A973",
            DateTime.Parse("2023-02-05T17:15:37.63"),
            DateTime.Parse("2023-02-05T17:15:37.63"),
            0);

        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await SendAsync(
            HttpMethod.Get,
            "/api/Orders/Get:2a5e24e2-1af2-45ca-b556-75774b88919d");

        // Assert.
        await AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
}