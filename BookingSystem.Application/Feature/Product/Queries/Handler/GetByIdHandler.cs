using AutoMapper;
using BookingSystem.Application.Feature.Product.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Queries.Handler
{
    internal class GetByIdHandler : IRequestHandler<GetByIdRequest, ProductQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductQuery> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetAsync(p => p.Id == request.id, includes: ["Category"]);
                return _mapper.Map<ProductQuery>(product);
            }
            catch
            {
                return new ProductQuery();
            }
        }
    }
}
