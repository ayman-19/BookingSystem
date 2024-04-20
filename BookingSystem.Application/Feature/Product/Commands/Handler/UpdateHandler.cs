using AutoMapper;
using BookingSystem.Application.Feature.Product.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Commands.Handler
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, ProductQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductQuery> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var oldProduct = await _unitOfWork.Products.GetAsync(p => p.Id == request.id);
                var product = _mapper.Map(request.Command, oldProduct);
                await _unitOfWork.Products.UpdateAsync(product);
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
