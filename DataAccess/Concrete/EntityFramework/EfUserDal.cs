using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DataTransferObjects;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, FarmManagementContext>, IUserDal
    {
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            await using var context = new FarmManagementContext();
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

        public async Task<UserDetailDto> GetUserDetails(Expression<Func<UserDetailDto, bool>> filter = null)
        {
            await using var context = new FarmManagementContext();
            
            var result = from u in context.Users
                join userOperationClaim in context.UserOperationClaims
                    on u.Id equals userOperationClaim.UserId
                join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals
                    operationClaim.Id
                select new UserDetailDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    City = u.City,
                    District = u.District,
                    Address = u.Address,
                    ZipCode = u.ZipCode,
                    ImagePath = context.UserImages.Where(im => im.UserId == u.Id).Select(im => im.ImagePath)
                        .SingleOrDefault(),
                    Profit = context.Orders.Where(o => o.SellerId == u.Id).Where(o => o.Status == 6).Sum(x => x.Price),
                    TotalSales = context.MilkSales.Count(m => m.SellerId == u.Id),
                    CustomerCount = context.Customers.Count(c => c.OwnerId == u.Id),
                    BullCount = context.Bulls.Count(b => b.OwnerId == u.Id),
                    CalfCount = context.Calves.Count(c => c.OwnerId == u.Id),
                    CowCount = context.Cows.Count(c => c.OwnerId == u.Id),
                    SheepCount = context.Sheeps.Count(s => s.OwnerId == u.Id),
                    AnimalCount = context.Cows.Count(cows => cows.OwnerId == u.Id) +
                                  context.Calves.Count(calves => calves.OwnerId == u.Id) +
                                  context.Bulls.Count(bull => bull.OwnerId == u.Id) +
                                  context.Sheeps.Count(sheep => sheep.OwnerId == u.Id),
                    Role = operationClaim.Name,
                    SuccessfulSales = context.Orders.Where(o => o.SellerId == u.Id).Count(o => o.Status == 6),
                    PendingOrders = context.Orders.Where(o => o.SellerId == u.Id).Count(o => o.Status == 2),
                    CanceledOrders = context.Orders.Where(o => o.SellerId == u.Id).Count(o => o.Status == 1),
                    ApprovedOrders = context.Orders.Where(o => o.SellerId == u.Id).Count(o => o.Status == 3),
                    DeliveryOrders = context.Orders.Where(o => o.SellerId == u.Id).Count(o => o.Status == 5)
                };

            return filter == null ? result.SingleOrDefault() : result.Where(filter).SingleOrDefault();
        }

        public async Task UpdateUser(UserForEdit userForEdit)
        {
           await using var context = new FarmManagementContext();

            var user = context.Users.SingleOrDefault(u => u.Id == userForEdit.Id);

            if (user != null)
            {
                user.FirstName = userForEdit.FirstName;
                user.LastName = userForEdit.LastName;
                user.PhoneNumber = userForEdit.PhoneNumber;
                user.City = userForEdit.City;
                user.District = userForEdit.District;
                user.Address = userForEdit.Address;
            }

            await context.SaveChangesAsync();
        }

        public async Task SetClaims(int id)
        {
           await using var context = new FarmManagementContext();

           UserOperationClaim userOperationClaim = new UserOperationClaim {UserId = id, OperationClaimId = 3};


            context.UserOperationClaims.Add(userOperationClaim);

           await context.SaveChangesAsync();
        }
    }
}