using AutoMapper;
using BookingSystem.Application.Feature.Category.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Commands.Handler
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, CategoryQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryQuery> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var oldCategory = await _unitOfWork.Categories.GetAsync(cat => cat.Id == request.id);
                var category = _mapper.Map(request.Command, oldCategory);
                await _unitOfWork.Categories.UpdateAsync(category);
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
