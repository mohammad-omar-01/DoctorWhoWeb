using FluentValidation;
using DoctorWho.DTOs.Models;

namespace DoctorWho.Web.Validators
{
    public class DoctorDtoValidator : AbstractValidator<DoctorDto>
    {
        public DoctorDtoValidator()
        {
            RuleFor(dto => dto.DoctorName)
                .NotEmpty().WithMessage("Doctor name is required.");
                
            RuleFor(dto => dto.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThanOrEqualTo(System.DateTime.Today).WithMessage("Birth date cannot be in the future.");

            RuleFor(dto => dto.FirstEpisodeDate)
                .NotEmpty().WithMessage("First episode date is required.")
                .LessThanOrEqualTo(System.DateTime.Today).WithMessage("First episode date cannot be in the future.")
                .GreaterThanOrEqualTo(dto => dto.BirthDate).WithMessage("First episode date must be after or equal to birth date.");

            RuleFor(dto => dto.LastEpisodeDate)
                .NotEmpty().WithMessage("Last episode date is required.")
                .LessThanOrEqualTo(System.DateTime.Today).WithMessage("Last episode date cannot be in the future.")
                .GreaterThanOrEqualTo(dto => dto.FirstEpisodeDate).WithMessage("Last episode date must be after or equal to first episode date.");
        }
    }
}
