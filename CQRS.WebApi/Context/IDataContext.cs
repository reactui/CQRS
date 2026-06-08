using Api.CQRS.WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.CQRS.WebApi.Infrastructure.Context
{
    public interface IDataContext
    {
        DbSet<Contact> Contacts { get; set; }

        Task<int> SaveChangesAsync();
    }
}