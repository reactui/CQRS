using System;

namespace Invelop.CQRS.WebApi.Domain.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string IBAN { get; set; }
    }
}
