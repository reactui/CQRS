using FluentValidation;
using Invelop.CQRS.WebApi.Features.ContactFeatures.Commands;

namespace Invelop.CQRS.WebApi.Validators
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(c => c.Firstname).NotEmpty();
            RuleFor(c => c.Surname).NotEmpty();            
        }
    }
}
