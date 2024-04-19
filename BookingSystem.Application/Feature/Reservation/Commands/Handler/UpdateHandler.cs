using AutoMapper;
using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Commands.Handler
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, ReservationQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationQuery> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reserve = await _unitOfWork.Reservations.GetAsync(re => re.Id == request.id);
                var updateReservation = _mapper.Map(request.Command, reserve);
                await _unitOfWork.Reservations.UpdateAsync(updateReservation);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReservationQuery>(updateReservation);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new ReservationQuery();
            }
        }
    }
}
