using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductById;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public sealed class ProductsController : ApiController
{
    /// <summary>
    /// Gets the product with the specified identifier, if it exists.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product with the specified identifier, if it exists.</returns>
    [HttpGet("Get:{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(productId);
        var product = await Sender.Send(query, cancellationToken);
        return Ok(product);
    }

    /// <summary>
    /// Creates a new product based on the specified request.
    /// </summary>
    /// <param name="request">The create product request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created product.</returns>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateProductCommand>();
        var productId = await Sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProduct), new { productId }, productId);
    }

    /// <summary>
    /// Updates a product based on the specified request.
    /// </summary>
    /// <param name="request">The update product request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated product.</returns>
    [HttpPut("Update")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateProductCommand>();
        var product = await Sender.Send(command, cancellationToken);
        return Ok(product);
    }

    /// <summary>
    /// Deletes an product based on the specified request.
    /// </summary>
    /// <param name="request">The delete product request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>If the product was deleted successfully.</returns>
    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(
        [FromBody] DeleteProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<DeleteProductCommand>();
        var deleted = await Sender.Send(command, cancellationToken);
        return Ok(deleted);
    }
}
