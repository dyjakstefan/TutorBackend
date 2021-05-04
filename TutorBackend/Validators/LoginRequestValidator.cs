using FluentValidation;
using TutorBackend.Core.Requests;

namespace TutorBackend.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty();
            RuleFor(request => request.Password)
                .NotEmpty();
        }
    }
}
