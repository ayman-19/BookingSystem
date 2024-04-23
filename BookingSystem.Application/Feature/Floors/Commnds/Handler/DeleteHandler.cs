using AutoMapper;
using BookingSystem.Application.Feature.Floors.Commnds.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Commnds.Handler
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, FloorQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FloorQuery> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var floor = await _unitOfWork.Floors.GetAsync(p => p.Id == request.id, includes: ["Rooms"]);
                await _unitOfWork.Floors.DeleteAsync(floor);
                if (floor.Rooms != null)
                    await _unitOfWork.Rooms.DeleteRangeAsync(floor.Rooms);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<FloorQuery>(floor);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new();
            }
        }
    }
}
