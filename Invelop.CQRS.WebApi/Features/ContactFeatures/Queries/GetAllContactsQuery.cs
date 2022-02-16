using Invelop.CQRS.WebApi.Domain.Models;
using Invelop.CQRS.WebApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Application.Features.ContactFeatures.Queries
{
    public class GetAllContactsQuery : IRequest<IEnumerable<Contact>>
    {

        public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery,IEnumerable<Contact>>
        {
            private readonly IDataContext _context;
            public GetAllContactsQueryHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Contact>> Handle(GetAllContactsQuery query, CancellationToken cancellationToken)
            {
                var contactList = await _context.Contacts.ToListAsync();
                if (contactList == null)
                {
                    return null;
                }
                return contactList.AsReadOnly();
            }
        }
    }
}
