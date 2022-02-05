using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IUserImageService
    {
        Task<IResult> Add(IFormFile file,UserImage userImage,int id,string securityKey);
        Task<IResult> Delete(UserImage userImage,int id,string securityKey);
        Task<IResult> Update(IFormFile file,UserImage userImage,int id,string securityKey);
        Task<IDataResult<UserImage>> Get(int id);
        Task<IDataResult<List<UserImage>>> GetAll();
        Task<IDataResult<UserImage>> GetImagesByUserId(int id,string securityKey);
    }
}