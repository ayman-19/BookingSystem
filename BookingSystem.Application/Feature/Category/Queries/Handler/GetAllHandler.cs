using AutoMapper;
using BookingSystem.Application.Feature.Category.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Queries.Handler
{
    internal class GetAllHandler : IRequestHandler<GetAllRequest, List<CategoryQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryQuery>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync();
                return _mapper.ProjectTo<CategoryQuery>(categories).ToList();
            }
            catch
            {
                return new List<CategoryQuery>();
            }
        }
    }
}
