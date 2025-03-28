using DrugIndication.Application.Dtos;
using DrugIndication.Domain.Constants;
using FluentValidation;

namespace DrugIndication.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            Include(new UserDtoValidator());

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == Roles.Admin || role == Roles.User)
                .WithMessage("Role must be either 'Admin' or 'User'.");
        }
    }
}
