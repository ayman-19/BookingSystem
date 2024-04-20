using AutoMapper;
using BookingSystem.Application.Feature.Product.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Commands.Handler
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, ProductQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductQuery> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetAsync(p => p.Id == request.id, includes: ["Category"]);
                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProductQuery>(product);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new ProductQuery();
            }
        }
    }
}
