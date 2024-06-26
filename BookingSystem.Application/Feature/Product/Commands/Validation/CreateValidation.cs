﻿using BookingSystem.Application.Feature.Product.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Product.Commands.Validation
{
    public class CreateValidation : AbstractValidator<CreateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }

        private void CustomValidation()
        {
            RuleFor(req => req.Command.Name)
                .NotEmpty().WithMessage("Name Not Empty")
                .NotNull().WithMessage("Name Not Null")
                .MustAsync(async (name, CancellationToken) => !await _unitOfWork.Products.IsAnyExistAsync(r => r.Name == name)).WithMessage("Name Is Exist");

            RuleFor(req => req.Command.Quantity)
                .NotEmpty().WithMessage("Quantity Not Empty")
                .NotNull().WithMessage("Quantity Not Null")
                .GreaterThan(0).WithMessage("Quantity Not Nigetive");

            RuleFor(req => req.Command.Price)
             .NotEmpty().WithMessage("Price Not Empty")
             .NotNull().WithMessage("Price Not Null")
             .GreaterThan(0).WithMessage("Price Not Nigetive");

            RuleFor(req => req.Command.CategoryId)
               .NotEmpty().WithMessage("Category Not Empty")
               .NotNull().WithMessage("Category Not Null")
               .MustAsync(async (id, CancellationToken) => await _unitOfWork.Categories.IsAnyExistAsync(r => r.Id == id)).WithMessage("Category Is Not Exist");
        }
    }
}
