using FluentValidation;
using DoctorWho.DTOs;
using DoctorWho.DTOs.Models;

namespace DoctorWho.Web.Validators
{
    public class DoctorDtoValidator : AbstractValidator<DoctorDto>
    {
        public DoctorDtoValidator()
        {
            RuleFor(dto => dto.DoctorName)
                .NotEmpty().WithMessage("Doctor name is required.");

            RuleFor(dto => dto.DoctorNumber).NotEmpty().WithMessage("Doctor Number is requierd");

            RuleFor(dto => dto.LastEpisodeDate)
                .Empty()
                .When(dto => !dto.FirstEpisodeDate.HasValue);

            RuleFor(dto => dto.LastEpisodeDate).GreaterThan(dto => dto.FirstEpisodeDate);

        }
    }
}
