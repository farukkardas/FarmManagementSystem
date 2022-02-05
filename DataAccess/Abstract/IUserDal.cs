using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract

{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
        
        Task<UserDetailDto> GetUserDetails(Expression<Func<UserDetailDto, bool>> filter = null);

        Task UpdateUser(UserForEdit userForEdit);

        void SetClaims(int id);
    }
}