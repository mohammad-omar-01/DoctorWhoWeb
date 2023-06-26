using DoctorWho.DTOs.Models;
using FluentValidation;

namespace DoctorWho.Web.Validators
{
    public class EpisodeDtoValidator : AbstractValidator<EpisodeCreationRequesetDTO>
    {
        public EpisodeDtoValidator()
        {
            RuleFor(dto => dto.AuthorId).NotEmpty().WithMessage("AuthorId is required.");
            RuleFor(dto => dto.DoctorId).NotEmpty().WithMessage("DoctorId is required.");
            RuleFor(dto => dto.SeriesNumber)
                .MaximumLength(10)
                .WithMessage("SeriesNumber must be 10 characters long.");
            RuleFor(dto => dto.EpisodeNumber)
                .GreaterThan(0)
                .WithMessage("EpisodeNumber must be greater than 0.");
        }
    }
}
