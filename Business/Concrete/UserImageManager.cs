using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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
        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperations("user,admin")]
        public async Task<IResult> Add(IFormFile file, UserImage userImage, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<UserImage>>(conditionResult.Message);
            }

            userImage.ImagePath = await FileHelper.Add(file);
            userImage.ImageDate = DateTime.Now;
            await _userImageDal.Add(userImage);
            return new SuccessResult($"Image is {file.FileName} {Messages.SuccessfullyAdded}");
        }


        [SecuredOperations("user,admin")]
        public async Task<IResult> Delete(UserImage userImage, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<UserImage>>(conditionResult.Message);
            }

            FileHelper.Delete(userImage.ImagePath);
            await _userImageDal.Delete(userImage);
            return new SuccessResult($"Image {Messages.SuccessfullyDeleted}");
        }

        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperations("user,admin")]
        public async Task<IResult> Update(IFormFile file, UserImage userImage, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<UserImage>>(conditionResult.Message);
            }

            var user = await _userImageDal.Get(u => u.UserId == id);

            if (user != null)
            {
                FileHelper.Delete(user.ImagePath);
                await _userImageDal.Delete(user);
            }


            userImage.ImagePath = await FileHelper.Add(file);
            userImage.ImageDate = DateTime.Now;
            await _userImageDal.Add(userImage);
            return new SuccessResult($"Image {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<UserImage>> Get(int id)
        {
            var result = await _userImageDal.Get(u => u.Id == id);
            return new SuccessDataResult<UserImage>(result);
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<List<UserImage>>> GetAll()
        {
            var result = await _userImageDal.GetAll();

            return new SuccessDataResult<List<UserImage>>(result);
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<UserImage>> GetImagesByUserId(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<UserImage>(conditionResult.Message);
            }

            var result = await _userImageDal.Get(u => u.UserId == id);

            return new SuccessDataResult<UserImage>(result);
        }
    }
}