using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Car;

namespace TurboProject.BusinessLayer.Validators.Car
{
    public class CreateCarRequestDtoValidator:AbstractValidator<CreateCarRequestDto>
    {
        public CreateCarRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ModelId)
                .GreaterThan(0).WithMessage("Model is required.");

            RuleFor(x => x.EngineSizeId)
                .GreaterThan(0).WithMessage("Engine size is required.");

            RuleFor(x => x.BodyTypeId)
                .GreaterThan(0).WithMessage("Body type is required.");

            RuleFor(x => x.FuelTypeId)
                .GreaterThan(0).WithMessage("Fuel type is required.");

            RuleFor(x => x.TransmissionId)
                .GreaterThan(0).WithMessage("Transmission type is required.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("City is required.");

            RuleFor(x => x.StatusId)
                .GreaterThan(0).WithMessage("Status is required.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage($"Year must be between 1900 and {DateTime.Now.Year}");

            RuleFor(x => x.VinCode)
                .NotEmpty().WithMessage("VIN code is required.")
                .Length(17).WithMessage("VIN code must be exactly 17 characters.");

            RuleFor(x => x.SeatNumber)
                .GreaterThan(0).WithMessage("Seat number must be positive.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0).WithMessage("Mileage cannot be negative.");

            RuleFor(x => x.HP)
                .GreaterThan(0).WithMessage("Horsepower must be positive.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description is too long.");
        }
    }
}
