using DoctorWho.DTOs.Models;
using FluentValidation;

namespace DoctorWho.web.Validators
{
    public class EnemyDTOValidator : AbstractValidator<EnemyDTO>
    {
        public EnemyDTOValidator()
        {
            RuleFor(dto => dto.EnemyName).NotEmpty().WithMessage("AuthorId is required.");
            RuleFor(dto => dto.EnemyId).NotEmpty().WithMessage("DoctorId is required.");
        }
    }
}
