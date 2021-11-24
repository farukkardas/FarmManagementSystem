using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IUserImageService
    {
        IResult Add(IFormFile file, UserImage userImage,int id,string securityKey);
        IResult Delete(UserImage userImage,int id,string securityKey);
        IResult Update(IFormFile file, UserImage userImage,int id,string securityKey);
        IDataResult<UserImage> Get(int id);
        IDataResult<List<UserImage>> GetAll();
        IDataResult<UserImage> GetImagesByUserId(int id,string securityKey);
    }
}