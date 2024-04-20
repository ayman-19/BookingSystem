using AutoMapper;
using BookingSystem.Application.Feature.Product.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Queries.Handler
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, List<ProductQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductQuery>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync(includes: ["Category"]);
                return _mapper.ProjectTo<ProductQuery>(products).ToList();
            }
            catch
            {
                return new List<ProductQuery>();
            }
        }
    }
}
