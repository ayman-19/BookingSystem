using AutoMapper;
using BookingSystem.Application.Feature.Rooms.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Queries.Handler
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, List<RoomQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<RoomQuery>> Handle(GetAllRequest request, CancellationToken cancellationToken)
            => _mapper.ProjectTo<RoomQuery>(await _unitOfWork.Rooms.GetAllAsync(includes: ["Floor"])).ToList();
    }
}
