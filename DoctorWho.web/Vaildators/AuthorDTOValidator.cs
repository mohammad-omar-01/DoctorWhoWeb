using DoctorWho.DTOs.Models;
using FluentValidation;

namespace DoctorWho.web.Validators
{
    public class AuthorDTOValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorDTOValidator()
        {
            RuleFor(dto => dto.AuthorName).NotEmpty().WithMessage("AuthorId is required.");
            RuleFor(dto => dto.AuthorId).NotEmpty().WithMessage("DoctorId is required.");
        }
    }
}
