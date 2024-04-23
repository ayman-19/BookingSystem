using AutoMapper;
using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Queries.Handler
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, UserReservedRespose>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserReservedRespose> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return _mapper.Map<UserReservedRespose>(await _unitOfWork.Users.GetAsync(user => user.Id == request.id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
