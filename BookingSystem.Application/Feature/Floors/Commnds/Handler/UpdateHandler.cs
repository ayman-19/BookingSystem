using AutoMapper;
using BookingSystem.Application.Feature.Floors.Commnds.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Commnds.Handler
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, FloorQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FloorQuery> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var oldFloor = await _unitOfWork.Floors.GetAsync(p => p.Id == request.id);
                var floor = _mapper.Map(request.Command, oldFloor);
                await _unitOfWork.Floors.UpdateAsync(floor);
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
