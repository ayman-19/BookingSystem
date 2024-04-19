using AutoMapper;
using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Commands.Handler
{
    public class CreateHandler : IRequestHandler<CreateRequest, ReservationQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationQuery> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reserve = _mapper.Map<Domain.Model.Reservation>(request.Command);
                await _unitOfWork.Reservations.AddAsync(reserve);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReservationQuery>(reserve);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new ReservationQuery();
            }
        }
    }
}
