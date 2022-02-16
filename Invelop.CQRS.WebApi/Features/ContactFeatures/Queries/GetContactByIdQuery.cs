using Invelop.CQRS.WebApi.Domain.Models;
using Invelop.CQRS.WebApi.Infrastructure.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Infrastructure.Features.ContactFeatures.Queries
{
    public class GetContactByIdQuery : IRequest<Contact>
    {
        public int Id { get; set; }
        public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact>
        {
            private readonly IDataContext _context;
            public GetContactByIdQueryHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<Contact> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
            {
                var contact =  _context.Contacts.Where(a => a.Id == query.Id).FirstOrDefault();
                //if (contact == null) return null;
                return contact;
            }
        }
    }
}
