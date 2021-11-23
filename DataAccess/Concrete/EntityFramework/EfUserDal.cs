using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DataTransferObjects;
using System.Linq.Expressions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, FarmManagementContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using var context = new FarmManagementContext();
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim
                {
                    Id = operationClaim.Id,
                    Name = operationClaim.Name
                };
            return result.ToList();
        }

        public UserDetailDto GetUserDetails(Expression<Func<UserDetailDto, bool>> filter = null)
        {
            using var context = new FarmManagementContext();
            
            var result = from u in context.Users
                select new UserDetailDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    City = u.City,
                    District = u.District,
                    Address =  u.Address,
                    Profit = context.MilkSales.Where(s => s.SellerId == u.Id).Sum(x => x.SalePrice),
                    TotalSales = context.MilkSales.Count(m => m.SellerId == u.Id),
                    CustomerCount = context.Customers.Count(c => c.OwnerId == u.Id),
                    BullCount = context.Bulls.Count(b=>b.OwnerId == u.Id),
                    CalfCount = context.Calves.Count(c=>c.OwnerId == u.Id),
                    CowCount = context.Cows.Count(c=>c.OwnerId == u.Id),
                    SheepCount = context.Sheeps.Count(s=>s.OwnerId == u.Id),
                    AnimalCount = context.Cows.Count(cows => cows.OwnerId == u.Id) +
                                  context.Calves.Count(calves => calves.OwnerId == u.Id) +
                                  context.Bulls.Count(bull => bull.OwnerId == u.Id) +
                                  context.Sheeps.Count(sheep => sheep.OwnerId == u.Id)
                                  
                };
            
            return filter == null ? result.FirstOrDefault() : result.Where(filter).FirstOrDefault();
        }

        
    }
}