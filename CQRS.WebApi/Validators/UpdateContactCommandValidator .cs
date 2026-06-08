using FluentValidation;
using Api.CQRS.WebApi.Features.ContactFeatures.Commands;

namespace Api.CQRS.WebApi.Validators
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
