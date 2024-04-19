using AutoMapper;
using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Commands.Handler
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, RoomQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<RoomQuery> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await _unitOfWork.Rooms.GetAsync(r => r.Id == request.id);
                var updateRoom = _mapper.Map(request.Command, room);
                await _unitOfWork.Rooms.UpdateAsync(updateRoom);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<RoomQuery>(updateRoom);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new RoomQuery();
            }
        }
    }
}
