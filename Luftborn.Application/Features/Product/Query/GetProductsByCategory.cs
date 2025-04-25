using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using MapsterMapper;
using MediatR;

namespace Luftborn.Application.Features.Product.Query;

public record GetProductsByCategoryQuery(string CategoryName) : IRequest<Result<List<ProductDto>>>;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<List<ProductDto>>>
{
    private readonly IECommerceUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByCategoryHandler(IECommerceUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetPagedListAsync(x => x.IsActive && x.Category.Name == request.CategoryName);
        if ( products is null )
            return Result<List<ProductDto>>.Failure("No Products Found", "404");
        
        var productDtos = products.Entities.Select(x => _mapper.Map<ProductDto>(x)).ToList();
        return Result<List<ProductDto>>.Success(productDtos);
    }
}