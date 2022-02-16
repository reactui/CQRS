using Invelop.CQRS.WebApi.Domain.Models;
using Invelop.CQRS.WebApi.Infrastructure.Context;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Application.Features.ContactFeatures.Commands
{
    public class CreateContactCommand : IRequest<int>
    {  
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string IBAN { get; set; }
        public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, int>
        {
            private readonly IDataContext _context;
            public CreateContactCommandHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateContactCommand command, CancellationToken cancellationToken)
            {
                var contact = new Contact();
                contact.Firstname = command.Firstname;
                contact.Surname = command.Surname;
                contact.DateOfBirth = command.DateOfBirth;
                contact.Address = command.Address;
                contact.Phone = command.Phone;
                contact.IBAN = command.IBAN;

                _context.Contacts.Add(contact);

                await _context.SaveChangesAsync();
                return contact.Id;
            }
        }
    }
}
