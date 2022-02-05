using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBullService
    {
        Task<IDataResult<List<Bull>>> GetAll();
        Task<IDataResult<Bull>> GetById(int id);
        Task<IResult> Add(Bull bull, int id, string securityKey);
        Task<IResult> Delete(Bull bull, int id, string securityKey);
        Task<IResult> Update(Bull bull, int id, string securityKey);
        Task<IDataResult<List<Bull>>> GetUserBulls(int id, string securityKey);
    }
}