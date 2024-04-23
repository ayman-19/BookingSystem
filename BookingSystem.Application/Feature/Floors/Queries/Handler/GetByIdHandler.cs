using AutoMapper;
using BookingSystem.Application.Feature.Floors.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Queries.Handler
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, FloorDetails>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FloorDetails> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var Floor = await _unitOfWork.Floors.GetAsync(p => p.Id == request.id, includes: ["Rooms"]);
                return _mapper.Map<FloorDetails>(Floor);
            }
            catch
            {
                return new FloorDetails();
            }
        }
    }
}
