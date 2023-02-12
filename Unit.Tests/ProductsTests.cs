using Application.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Unit.Tests;

public class ProductsTests
{
    private readonly ProductsController _productsController;

    public ProductsTests()
    {
        _productsController = new ProductsController();
    }

    [Fact]
    public void Products_Controller_Instance_Should_Be_NotNull()
    {
        // Arrage        
        // Act
        // Assert
        Assert.NotNull(_productsController);
    }

    [Fact]
    public async void CreateProduct_Returns_Product_Id()
    {
        // Arrage
        var request = new CreateProductRequest(
            RandomString(10),
            RandomString(100),
            Random.NextDouble(),
            Random.Next(1000));

        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        // Act
        var result = await _productsController.CreateProduct(request, cts.Token);
        var objResult = result as ObjectResult;
        var guid = objResult is not null ? objResult.Value.ToString() : "";

        // Assert
        Assert.True(Guid.TryParse(guid, out Guid _guid));
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