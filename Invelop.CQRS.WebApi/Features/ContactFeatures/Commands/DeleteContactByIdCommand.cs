using Invelop.CQRS.WebApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Features.ContactFeatures.Commands
{
    public class DeleteContactByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteContactByIdCommandHandler : IRequestHandler<DeleteContactByIdCommand, int>
        {
            private readonly IDataContext _context;
            public DeleteContactByIdCommandHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteContactByIdCommand command, CancellationToken cancellationToken)
            {
                var contact = await _context.Contacts.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (contact == null)
                {
                    return default;
                }

                _context.Contacts.Remove(contact);

                await _context.SaveChangesAsync();
                return contact.Id;
            }
        }
    }
}
