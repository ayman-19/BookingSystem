using AutoMapper;
using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Commands.Handler
{
    public class CreateHandler : IRequestHandler<CreateRequest, RoomQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<RoomQuery> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var room = _mapper.Map<Room>(request.Command);
                await _unitOfWork.Rooms.AddAsync(room);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<RoomQuery>(room);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new RoomQuery();
            }
        }
    }
}
