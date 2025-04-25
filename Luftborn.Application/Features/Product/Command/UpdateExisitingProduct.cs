using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using Mapster;
using MediatR;

namespace Luftborn.Application.Features.Product.Command;

public record UpdateExisitingProductCommand(int Id , ProductDto ProductDto) : IRequest<Result<bool>>;

public class UpdateExisitingProductHandler : IRequestHandler<UpdateExisitingProductCommand, Result<bool>>
{
    private readonly IECommerceUnitOfWork _unitOfWork;


    public UpdateExisitingProductHandler(IECommerceUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateExisitingProductCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0 || request.ProductDto is null)  
            return Result<bool>.Failure("Failed to update product", "400"); 
        var existingProduct = await _unitOfWork.Products.GetSingleAsync(x=>x.Id == request.Id);
        if (existingProduct is null) 
            return Result<bool>.Failure("Product not found", "404");
        var product = request.ProductDto.Adapt<Core.DomainEntities.Product>();
         _unitOfWork.Products.Update(product);
        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true, "Product Updated");
    }
}