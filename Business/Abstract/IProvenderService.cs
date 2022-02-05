using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProvenderService
    {
        Task<IDataResult<List<Provender>>> GetAll();
        Task<IDataResult<Provender>> GetById(int id);
        Task<IResult> Add(Provender cow,int id,string securityKey);
        Task<IResult> Delete(Provender cow,int id,string securityKey);
        Task<IResult> Update(Provender cow,int id,string securityKey);
        Task<IDataResult<List<Provender>>> GetUserProvenders(int id, string securityKey);
    }
}