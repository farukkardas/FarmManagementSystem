using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISheepService
    {
        Task<IDataResult<List<Sheep>>> GetAll();
        Task<IDataResult<Sheep>> GetById(int id);
        Task<IResult> Add(Sheep sheep,int id, string securityKey);
        Task<IResult> Delete(Sheep sheep,int id, string securityKey);
        Task<IResult> Update(Sheep sheep,int id, string securityKey);
        Task<IDataResult<List<Sheep>>> GetUserSheeps(int id, string securityKey);
    }
}