﻿using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Rooms.Commands.Validation
{
    public class DeleteValidation : AbstractValidator<DeleteRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }
        private void CustomValidation()
        {
            RuleFor(r => r.id)
               .NotEmpty().WithMessage("Id Not Empty")
                .NotNull().WithMessage("Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Rooms.IsAnyExistAsync(r => r.Id == id)).WithMessage("Room Is Not Exist");
        }

    }
}
