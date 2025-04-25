using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using MapsterMapper;
using MediatR;

namespace Luftborn.Application.Features.Product.Query;

public record GetProductByIdQuery(int Id) : IRequest<Result<ProductDto>>;


public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IECommerceUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IECommerceUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetSingleAsync(x=>x.Id == request.Id && x.IsActive);
        if (product == null) return Result<ProductDto>.Failure("Product not found", "404");
        var dto = _mapper.Map<ProductDto>(product);
        return Result<ProductDto>.Success(dto);
    }
}

