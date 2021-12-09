using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
          ;
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

       // [SecuredOperations("admin")]
        [CacheAspect(20)]
        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            return new SuccessDataResult<User>(result);
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult($"User{Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult($"User{Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("user")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult($"User{Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserUpdateValidator))]
        public IResult UpdateUser(UserForEdit userForEdit)
        {
            _userDal.UpdateUser(userForEdit);
            return new SuccessResult($"User{Messages.SuccessfullyUpdated}");
        }


        [SecuredOperations("admin,user,customer")]
        public IDataResult<UserDetailDto> GetUserDetails(int id,string securityKey)
        {
            var result = _userDal.GetUserDetails(u => u.Id == id);

            return new SuccessDataResult<UserDetailDto>(result, "Data was successfully fetched.");
        }
        
        
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
        
        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}