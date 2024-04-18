using AutoMapper;
using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Commands.Handler
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, Query>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Query> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await _unitOfWork.Rooms.GetAsync(r => r.Id == request.id);
                await _unitOfWork.Rooms.DeleteAsync(room);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Query>(room);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return new Query();
            }
        }
    }
}
