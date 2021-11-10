using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract

{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

        UserDetailDto GetUserDetails(Expression<Func<UserDetailDto, bool>> filter = null);

    }
}