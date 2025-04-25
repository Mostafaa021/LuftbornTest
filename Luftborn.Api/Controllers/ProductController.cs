using Asp.Versioning;
using Luftborn.Application.Common.Factories;
using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Application.Features.Product.Command;
using Luftborn.Application.Features.Product.Query;
using Luftborn.Core.DomainEntities.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuftbornTestApp.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Policy = "Admin")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetProductById/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductDto>))]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<ProductDto>>))]
    [HttpGet("GetAllActiveProducts")]
    public async Task<IActionResult> GetAllActiveProducts()
    {
        var result = await _mediator.Send(new GetAllActiveProductsQuery());
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<ProductDto>>))]
    [HttpGet("GetAllActiveProductsByCategory/{categoryName}")]
    public async Task<IActionResult> GetAllActiveProductsByCategory(string categoryName)
    {
        var result = await _mediator.Send(new GetProductsByCategoryQuery(categoryName));
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct(ProductDto dto)
    {
        var result = await _mediator.Send(new CreateProductCommand(dto));
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [HttpPut("UpdateProduct/{id:int}")]
    public async Task<IActionResult> UpdateProduct( int id , ProductDto dto)
    {
        var result = await _mediator.Send(new UpdateExisitingProductCommand(id,dto));
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [HttpDelete("DeleteProduct/{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _mediator.Send(new DeleteExisitingProductCommand(id));
        return Ok(result);
    }
    
}