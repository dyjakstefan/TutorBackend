using FluentValidation;
using TutorBackend.Core.Requests;

namespace TutorBackend.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
            RuleFor(request => request.FirstName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(request => request.LastName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
        }
    }
}
