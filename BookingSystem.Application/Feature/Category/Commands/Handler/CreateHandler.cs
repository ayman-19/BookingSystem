using AutoMapper;
using BookingSystem.Application.Feature.Category.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Commands.Handler
{
    public class CreateHandler : IRequestHandler<CreateRequest, CategoryQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryQuery> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _mapper.Map<Domain.Model.Category>(request.Command);
                await _unitOfWork.Categories.AddAsync(category);
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
