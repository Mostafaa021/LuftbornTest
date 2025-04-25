using Luftborn.Application.Common.Factories;
using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Application.Features.Product.Query;


public record GetAllActiveProductsQuery(): IRequest<Result<List<ProductDto>>>;


public class GetAllActiveProductsHandler : IRequestHandler<GetAllActiveProductsQuery, Result<List<ProductDto>>>
{
    private readonly IECommerceUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProductFilterStrategyFactory _strategyFactory;

    public GetAllActiveProductsHandler(
        IECommerceUnitOfWork unitOfWork,
        IMapper mapper , 
        IProductFilterStrategyFactory strategyFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _strategyFactory = strategyFactory;
    }

    public async Task<Result<List<ProductDto>>> Handle(GetAllActiveProductsQuery request, CancellationToken cancellationToken)
    {
        // Here Simulator of Strategy Pattern selected by factory pattern 
        var selectedStrategy = _strategyFactory.GetStrategy("active");
        var productsQuery =  _unitOfWork.Products.GetQueryable(x => x.IsActive);
        
        var filteredQuery = await selectedStrategy.ApplyFilter(productsQuery);
        var products = await  filteredQuery.ToListAsync(cancellationToken);
        if ( products.Count == 0)
            return Result<List<ProductDto>>.Failure("No Active Products Found", "404");
        
        var productDtos = products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
        return Result<List<ProductDto>>.Success(productDtos);
    }
}