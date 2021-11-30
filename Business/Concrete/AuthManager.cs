using System;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT.Abstract;
using Core.Utilities.Security.JWT.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DataTransferObjects;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<User> Register(UserRegisterDto userRegisterDto, string password)
        {
            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            
            _userService.Add(user);
            _userDal.SetClaims(user.Id);

            return new SuccessDataResult<User>(user, $"{user} , {Messages.SuccessfullyAdded}");
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByMail(userLoginDto.Email);


            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("User not found!");
            }

            GenerateRandomSecurityKey(userToCheck);

            IResult result = BusinessRules.Run(CheckUserStatus(userLoginDto.Email));

            if (result != null)
            {
                return new ErrorDataResult<User>($"{result.Message}");
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Wrong password!");
            }


            return new SuccessDataResult<User>(userToCheck);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("This user is exists!");
            }

            return new SuccessResult();
        }


        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken, "Successful login!");
        }

        private IResult CheckUserStatus(string email)
        {
            var user = _userService.GetByMail(email);

            if (user.Status == false)
            {
                return new ErrorDataResult<User>("Account is disabled.");
            }

            return new SuccessResult();
        }

        private void GenerateRandomSecurityKey(User user)
        {
            using var context = new FarmManagementContext();
            
            if (user == null || user.SecurityKeyExpiration > DateTime.Now)
            {
                return;
            }
            
            user.SecurityKey = RandomSecurityKey();
            user.SecurityKeyExpiration = DateTime.Now.AddDays(1);
               
            _userDal.Update(user);

            context.SaveChanges();
        }

        private string RandomSecurityKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[100];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public IResult UserOwnControl(int userId, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == userId);

            if (user == null)
            {
                return new ErrorResult("User not found");
            }

            if (user.SecurityKey != securityKey)
            {
                return new ErrorResult("You have not permission for this.");
            }

            return new SuccessResult();
        }

        public IResult CheckSecurityKeyOutdated(int id)
        {
            var result = _userDal.Get(u=>u.Id == id);

            if (result == null)
            {
                return new ErrorResult("User not found, logout");
            }

            if (result.SecurityKeyExpiration < DateTime.Now)
           {
               return new ErrorResult("Security key outdated");
           }

           return new SuccessResult();
        }
    }
}