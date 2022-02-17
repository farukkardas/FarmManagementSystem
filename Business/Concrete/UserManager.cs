using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<List<User>>> GetAll()
        {
            var result = await _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<User>> GetById(int id)
        {
            var result = await _userDal.Get(u => u.Id == id);
            return new SuccessDataResult<User>(result);
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public async Task<IResult> Add(User user)
        {
            await _userDal.Add(user);
            return new SuccessResult($"User{Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> Delete(User user)
        {
            await _userDal.Delete(user);
            return new SuccessResult($"User{Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("user")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public async Task<IResult> Update(User user)
        {
            await _userDal.Update(user);
            return new SuccessResult($"User{Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserUpdateValidator))]
        public async Task<IResult> UpdateUser(UserForEdit userForEdit)
        {
            await _userDal.UpdateUser(userForEdit);
            return new SuccessResult($"User{Messages.SuccessfullyUpdated}");
        }

        [FileLogger(typeof(UserDetailDto))]
        [SecuredOperations("admin,user,customer")]
        [CacheAspect(10)]
        public async Task<IDataResult<UserDetailDto>> GetUserDetails(int id, string securityKey)
        {
            var result = await _userDal.GetUserDetails(u => u.Id == id);

            return new SuccessDataResult<UserDetailDto>(result, "Data was successfully fetched.");
        }

        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperations("admin,user,customer")]
        public async Task<IResult> ChangeUserAddress(int id, string securityKey, int cityId, string fullAddress)
        {
            var user = await _userDal.Get(u => u.Id == id);
            user.City = cityId;
            user.Address = fullAddress;
            await _userDal.Update(user);
            return new SuccessResult("Address information is updated!");
        }


        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            return await _userDal.GetClaims(user);
        }

        public async Task<User> GetByMail(string email)
        {
            return await _userDal.Get(u => u.Email == email);
        }
    }
}