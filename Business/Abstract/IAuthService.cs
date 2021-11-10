using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userRegisterDto, string password);
        IDataResult<User> Login(UserLoginDto userLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        
     

    }
}