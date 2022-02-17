using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Logging;
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
        public async Task<IDataResult<User>> Register(UserRegisterDto userRegisterDto, string password)
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


            await _userService.Add(user);
            await _userDal.SetClaims(user.Id);
            await GenerateRandomSecurityKey(user);

            return new SuccessDataResult<User>(user, $"{user} , {Messages.SuccessfullyAdded}");
        }

        [FileLogger(typeof(UserLoginDto))]
        public async Task<IDataResult<User>> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = await _userService.GetByMail(userLoginDto.Email);


            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("User not found!");
            }

            await GenerateRandomSecurityKey(userToCheck);

            IResult result = BusinessRules.Run(await CheckUserStatus(userLoginDto.Email));

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

        public async Task<IResult> UserExists(string email)
        {
            if (await _userService.GetByMail(email) != null)
            {
                return new ErrorResult("This user is exists!");
            }

            return new SuccessResult();
        }


        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken, "Successful login!");
        }

        private async Task<IResult> CheckUserStatus(string email)
        {
            var user = await _userService.GetByMail(email);

            if (user.Status == false)
            {
                return new ErrorDataResult<User>("Account is disabled.");
            }

            return new SuccessResult();
        }

        private async Task GenerateRandomSecurityKey(User user)
        {
            await using var context = new FarmManagementContext();

            if (user == null || user.SecurityKeyExpiration > DateTime.Now)
            {
                return;
            }

            user.SecurityKey = await RandomSecurityKey();
            user.SecurityKeyExpiration = DateTime.Now.AddDays(1);

            await _userDal.Update(user);

            await context.SaveChangesAsync();
        }

        private async Task<string> RandomSecurityKey()
        {
            return await Task.Run((() =>
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
            }));
        }

        public async Task<IResult> UserOwnControl(int userId, string securityKey)
        {
            var user = await _userDal.Get(u => u.Id == userId);

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

        public async Task<IResult> CheckSecurityKeyOutdated(int id)
        {
            var result = await _userDal.Get(u => u.Id == id);

            if (result == null)
            {
                return new ErrorResult("User not found, logout");
            }

            if (result.SecurityKeyExpiration < DateTime.Now)
            {
                return new ErrorResult("Security key outdated");
            }

            return new SuccessResult("Security key is up to date.");
        }

     
    }
}