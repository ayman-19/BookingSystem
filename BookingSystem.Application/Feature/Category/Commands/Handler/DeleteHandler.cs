using AutoMapper;
using BookingSystem.Application.Feature.Category.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Commands.Handler
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, CategoryQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryQuery> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetAsync(cat => cat.Id == request.id);
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryQuery>(category);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new CategoryQuery();
            }
        }
    }
}
