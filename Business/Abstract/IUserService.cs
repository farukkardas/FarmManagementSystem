﻿using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
        List<OperationClaim> GetClaims(User user);
        User GetByMail(string email);
        IDataResult<UserDetailDto> GetUserDetails(int id,string securityKey);


    }
}