using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<User>>> GetAll();
        Task<IDataResult<User>> GetById(int id);
        Task<IResult> Add(User user);
        Task<IResult> Delete(User user);
        Task<IResult> Update(User user);
        Task<IResult> UpdateUser(UserForEdit userForEdit);
        Task<List<OperationClaim>> GetClaims(User user);
        Task<User> GetByMail(string email);
        Task<IDataResult<UserDetailDto>> GetUserDetails(int id,string securityKey);
        Task<IResult> ChangeUserAddress(int id, string securityKey, int cityId, string fullAddress);
    }
}