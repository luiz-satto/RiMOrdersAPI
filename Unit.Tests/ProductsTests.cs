using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

namespace Unit.Tests;

public class ProductsTests : TestBase
{
    [Fact]
    public async void CreateProduct_Should_ReturnHttpStatusCodeCreated()
    {
        // Arrage
        var expectedStatusCode = System.Net.HttpStatusCode.Created;
        var content = JsonContent.Create(new
        {
            Name = RandomString(10),
            Description = RandomString(1000),
            Price = Random.NextDouble(),
            Stock = Random.Next(1000)
        });

        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await SendAsync(HttpMethod.Post, $"/api/Products/Create", content);

        // Assert
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
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