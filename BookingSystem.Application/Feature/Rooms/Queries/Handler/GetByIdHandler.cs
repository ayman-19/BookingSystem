using AutoMapper;
using BookingSystem.Application.Feature.Rooms.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Queries.Handler
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, Query>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Query> Handle(GetByIdRequest request, CancellationToken cancellationToken) => _mapper.Map<Query>(await _unitOfWork.Rooms.GetAsync(r => r.Id == request.id));
    }
}
