using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
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

        public IResult Add(IFormFile file, [FromForm]UserImage userImage,int id,string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey)); 
            
            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }
            userImage.ImagePath = FileHelper.Add(file);
            userImage.ImageDate = DateTime.Now;
            _userImageDal.Add(userImage);
            return new SuccessResult($"Image is {Messages.SuccessfullyAdded}");
        }

        public IResult Delete(UserImage userImage,int id,string securityKey)
        {
            _authService.UserOwnControl(id,securityKey);
           FileHelper.Delete(userImage.ImagePath);
           _userImageDal.Delete(userImage);
           return new SuccessResult($"Image {Messages.SuccessfullyDeleted}");
        }

        public IResult Update(IFormFile file, UserImage userImage,int id,string securityKey)
        {
            _authService.UserOwnControl(id,securityKey);
            userImage.ImagePath = FileHelper.Add(file);
            userImage.ImageDate = DateTime.Now;
            _userImageDal.Update(userImage);
            return new SuccessResult($"Image {Messages.SuccessfullyUpdated}");

        }

        [SecuredOperations("admin")]
        public IDataResult<UserImage> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        [SecuredOperations("admin")]
        public IDataResult<List<UserImage>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<UserImage>> GetImagesByUserId(int userId,string securityKey)
        {
            throw new System.NotImplementedException();
        }
    }
}