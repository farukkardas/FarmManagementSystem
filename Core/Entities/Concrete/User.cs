using System;
using DataAccess.Entites;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
         public string PhoneNumber { get; set; }
         public int? City { get; set; }
         public string District { get; set; }
         public string Address { get; set; }
         public int? ZipCode { get; set; }
        public string SecurityKey { get; set; }
        public DateTime? SecurityKeyExpiration { get; set; }

    }
}