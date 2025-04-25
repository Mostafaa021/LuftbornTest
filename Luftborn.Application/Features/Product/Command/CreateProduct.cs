using Luftborn.Application.Common.Factories;
using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using MediatR;

namespace Luftborn.Application.Features.Product.Command;


public record CreateProductCommand(ProductDto ProductDto) : IRequest<Result<bool>>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<bool>>
{
    private readonly IECommerceUnitOfWork _unitOfWork;
    public CreateProductCommandHandler(IECommerceUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CreateProductCommand? request, CancellationToken cancellationToken)
    {
        if (request is null )  return Result<bool>.Failure("Invalid Data", "400"); 
        // simulation of implementation of a factory pattern
        var productFactory = ProductFactoryProvider.GetProductFactory(request.ProductDto.ProductType);
        var product = productFactory.CreateProduct(request.ProductDto);
        
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true, "Product Created");
    }
}

