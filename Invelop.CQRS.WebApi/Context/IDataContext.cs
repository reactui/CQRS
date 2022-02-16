using Invelop.CQRS.WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Infrastructure.Context
{
    public interface IDataContext
    {
        DbSet<Contact> Contacts { get; set; }

        Task<int> SaveChangesAsync();
    }
}