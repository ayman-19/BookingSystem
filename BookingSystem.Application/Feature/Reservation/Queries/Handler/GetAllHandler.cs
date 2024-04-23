using AutoMapper;
using BookingSystem.Application.Feature.Reservation.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Queries.Handler
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IQueryable<ReservationQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IQueryable<ReservationQuery>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reversation = await _unitOfWork.Reservations.GetAllAsync(includes: ["Users"]);
                return _mapper.ProjectTo<ReservationQuery>(reversation);
            }
            catch
            {
                throw new Exception("Error!");
            }
        }
    }
}
