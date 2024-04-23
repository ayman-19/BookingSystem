using AutoMapper;
using BookingSystem.Application.Feature.Floors.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Queries.Handler
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, List<FloorQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<FloorQuery>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var floors = await _unitOfWork.Floors.GetAllAsync();
                return _mapper.ProjectTo<FloorQuery>(floors).ToList();
            }
            catch
            {
                return new();
            }
        }
    }
}
