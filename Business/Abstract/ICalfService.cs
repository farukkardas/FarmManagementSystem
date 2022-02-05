using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICalfService
    {
        Task<IDataResult<List<Calves>>> GetAll();
        Task<IDataResult<Calves>> GetById(int id);
        Task<IResult> Add(Calves calves,int id ,string securityKey);
        Task<IResult> Delete(Calves calves,int id ,string securityKey);
        Task<IResult> Update(Calves calves,int id ,string securityKey);
        Task<IDataResult<List<Calves>>> GetUserCalves(int id, string securityKey);
    }
}