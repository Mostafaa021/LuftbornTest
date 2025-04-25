using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using MapsterMapper;

namespace Luftborn.Application.MeidatorSimulation;

// this for Simulating Mediator Pattern Implementation but in project i`m using MediatR
public interface IQuery<TResult>  // return Type
{
      
}
public interface ICommand // Return Nothing but Success or Failure
{
      
}
public interface ICommandHandler<in TCommand>  
    where TCommand : ICommand
{
    Result<bool> Handle(TCommand command);
}
public interface IQueryHandler<in TQuery , out TResult>
    where TQuery : IQuery<TResult>
    where TResult : class
{
    TResult Handle(TQuery query);
}

// That a Simulation of the Mediator Pattern Implementation Not actual Implementation used in Project
public sealed class GetProductsQuery : IQuery<List<ProductDto>>
{
    public int CategoryId { get; set; }
    public string SearchTerm { get; set; }
    
    public GetProductsQuery(int categoryId, string searchTerm)
    {
        CategoryId = categoryId;
        SearchTerm = searchTerm;
    }
    
    public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IECommerceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IECommerceUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<ProductDto> Handle(GetProductsQuery query)
        {
            var products =   _unitOfWork.Products.GetListAsync(x => x.IsActive && !x.IsDeleted).Result;
            var productsDto = new List<ProductDto>();
            foreach (var product in products)
            {
                productsDto.Add(_mapper.Map<ProductDto>(product));
            }
            return productsDto;
        }
    }
   
}
