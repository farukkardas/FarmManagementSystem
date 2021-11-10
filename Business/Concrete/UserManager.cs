using System;
using System.Collections.Generic;
using System.Linq;
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
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

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

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult($"User{Messages.SuccessfullyDeleted}");
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult($"User{Messages.SuccessfullyUpdated}");
        }

       

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }


        public IDataResult<UserDetailDto> GetUserDetails(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(CheckUserIdAndSecurityKey(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<UserDetailDto>(conditionResult.Message);
            }

            var result = _userDal.GetUserDetails(u => u.Id == id);

            return new SuccessDataResult<UserDetailDto>(result, "Data was successfully fetched.");
        }


        private IDataResult<UserDetailDto> CheckUserIdAndSecurityKey(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            var result = _userDal.GetUserDetails(x => x.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<UserDetailDto>($"User not found!");
            }

            if (result == null)
            {
                return new ErrorDataResult<UserDetailDto>("Error when getting user details!");
            }
            

            if (result.Id != user.Id)
            {
                return new ErrorDataResult<UserDetailDto>($" ID error : {Messages.AuthorizationDenied}");
            }

            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<UserDetailDto>($" email error : {Messages.AuthorizationDenied}");
            }

            return new SuccessDataResult<UserDetailDto>(result);
        }

        
    }
}