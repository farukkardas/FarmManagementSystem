using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class UserLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}