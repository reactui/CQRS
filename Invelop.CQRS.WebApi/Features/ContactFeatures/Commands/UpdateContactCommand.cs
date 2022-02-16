using Invelop.CQRS.WebApi.Infrastructure.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Features.ContactFeatures.Commands
{
    public class UpdateContactCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string IBAN { get; set; }
        public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, int>
        {
            private readonly IDataContext _context;
            public UpdateContactCommandHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
            {
                var contact = _context.Contacts.Where(a => a.Id == command.Id).FirstOrDefault();

                if (contact == null)
                {
                    return default;
                }
                else
                {
                    contact.Firstname = command.Firstname;
                    contact.Surname = command.Surname;
                    contact.DateOfBirth = command.DateOfBirth;
                    contact.Address = command.Address;
                    contact.Phone = command.Phone;
                    contact.IBAN = command.IBAN;
                    await _context.SaveChangesAsync();
                    return contact.Id;
                }
            }
        }
    }
}
