using AutoMapper;
using BookingSystem.Application.Feature.Product.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Commands.Handler
{
    public class CreateHandler : IRequestHandler<CreateRequest, ProductQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductQuery> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Domain.Model.Product>(request.Command);
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                var response = _mapper.Map<ProductQuery>(product);
                response.Category = await _unitOfWork.Categories.GetNameByIdAsync(request.Command.CategoryId);
                return response;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new ProductQuery();
            }
        }
    }
}
