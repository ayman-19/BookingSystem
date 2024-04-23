using AutoMapper;
using BookingSystem.Application.Feature.Floors.Commnds.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Commnds.Handler
{
    internal class CreateHandler : IRequestHandler<CreateRequest, FloorQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FloorQuery> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var floor = _mapper.Map<Floor>(request.Command);
                await _unitOfWork.Floors.AddAsync(floor);
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
