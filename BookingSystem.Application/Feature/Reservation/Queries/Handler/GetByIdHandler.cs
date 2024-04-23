using AutoMapper;
using BookingSystem.Application.Feature.Reservation.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Queries.Handler
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, ReservationQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationQuery> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reversation = await _unitOfWork.Reservations.GetAsync(re => re.Id == request.id, includes: ["Users.Room.Floor"]);
                return _mapper.Map<ReservationQuery>(reversation);
            }
            catch
            {
                throw new Exception("Error!");
            }
        }
    }
}
