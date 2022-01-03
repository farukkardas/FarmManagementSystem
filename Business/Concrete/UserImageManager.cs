using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Business.Concrete
{
    public class UserImageManager : IUserImageService
    {
        private readonly IUserImageDal _userImageDal;
        private readonly IAuthService _authService;

        public UserImageManager(IUserImageDal userImageDal, IAuthService authService)
        {
            _userImageDal = userImageDal;
            _authService = authService;
        }
        [TransactionScopeAspect]
        [SecuredOperations("user,admin")]
        public IResult Add(IFormFile file, UserImage userImage, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));
        
            if (conditionResult != null)
            {
                return new ErrorDataResult<List<UserImage>>(conditionResult.Message);
            }
        
            userImage.ImagePath =  FileHelper.Add(file);
            userImage.ImageDate = DateTime.Now;
            _userImageDal.Add(userImage);
            return new SuccessResult($"Image is {file.FileName} {Messages.SuccessfullyAdded}");
        }

      

        [SecuredOperations("user,admin")]
        public IResult Delete(UserImage userImage, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<UserImage>>(conditionResult.Message);
            }

            FileHelper.Delete(userImage.ImagePath);
            _userImageDal.Delete(userImage);
            return new SuccessResult($"Image {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("user,admin")]
        public IResult Update(IFormFile file, UserImage userImage, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<UserImage>>(conditionResult.Message);
            }

            var user = _userImageDal.Get(u => u.UserId == id);

            if (user != null)
            {
                FileHelper.Delete(user.ImagePath);
                _userImageDal.Delete(user);
            }
           

            userImage.ImagePath = FileHelper.Add(file);
            userImage.ImageDate = DateTime.Now;
            _userImageDal.Add(userImage);
            return new SuccessResult($"Image {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("admin")]
        public IDataResult<UserImage> Get(int id)
        {
            var result = _userImageDal.Get(u => u.Id == id);
            return new SuccessDataResult<UserImage>(result);
        }

        [SecuredOperations("admin")]
        public IDataResult<List<UserImage>> GetAll()
        {
            var result = _userImageDal.GetAll();

            return new SuccessDataResult<List<UserImage>>(result);
        }

        [SecuredOperations("admin")]
        public IDataResult<UserImage> GetImagesByUserId(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<UserImage>(conditionResult.Message);
            }

            var result = _userImageDal.Get(u => u.UserId == id);

            return new SuccessDataResult<UserImage>(result);
        }
    }
}