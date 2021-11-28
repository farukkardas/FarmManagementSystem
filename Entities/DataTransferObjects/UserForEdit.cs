using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class UserForEdit : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }

    }
}