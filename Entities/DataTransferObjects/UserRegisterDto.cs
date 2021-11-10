using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class UserRegisterDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}