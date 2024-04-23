using AutoMapper;
using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Commands.Handler
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, ReservationQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationQuery> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reverse = await _unitOfWork.Reservations.GetAsync(re => re.Id == request.id);
                await _unitOfWork.Reservations.DeleteAsync(reverse);
                await _unitOfWork.Rooms.MakeRoomIsNotBookedByReservationAsync(reverse.Id);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReservationQuery>(reverse);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new ReservationQuery();
            }
        }
    }
}
