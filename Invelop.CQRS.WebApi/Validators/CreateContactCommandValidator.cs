using Invelop.CQRS.WebApi.Application.Features.ContactFeatures.Commands;
using FluentValidation;

namespace Invelop.CQRS.WebApi.Validators
{
    public class CreateContactCommndValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommndValidator()
        {
            RuleFor(c => c.Firstname).NotEmpty();
            RuleFor(c => c.Surname).NotEmpty();
        }
    }
}
