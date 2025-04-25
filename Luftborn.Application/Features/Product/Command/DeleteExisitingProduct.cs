using Luftborn.Application.Common.Results;
using Luftborn.Core.Abstraction.Domain;
using MediatR;

namespace Luftborn.Application.Features.Product.Command;

public record DeleteExisitingProductCommand(int id) : IRequest<Result<bool>>;

public class DeleteExisitingProductCommandHandler : IRequestHandler<DeleteExisitingProductCommand, Result<bool>>
{
    private readonly IECommerceUnitOfWork _unitOfWork;

    public DeleteExisitingProductCommandHandler(IECommerceUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteExisitingProductCommand request, CancellationToken cancellationToken)
    {
        if (request.id == 0) 
            return Result<bool>.Failure("Invalid ID", "400");
        var existingProduct = await _unitOfWork.Products.GetSingleAsync(x=>x.Id == request.id);
        if (existingProduct is null) 
            return Result<bool>.Failure("Invalid Data", "400");
        existingProduct.IsDeleted = true; // soft delete
        _unitOfWork.Products.Update(existingProduct);
        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true, "Product Deleted");
    }
}