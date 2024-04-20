using AutoMapper;
using BookingSystem.Application.Feature.Category.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Queries.Handler
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, CategoryQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryQuery> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetAsync(cat => cat.Id == request.id);
                return _mapper.Map<CategoryQuery>(category);
            }
            catch
            {
                return new CategoryQuery();
            }
        }
    }
}
