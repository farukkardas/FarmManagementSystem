using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICowService
    {
        Task<IDataResult<List<Cow>>> GetAll();
        Task<IDataResult<Cow>> GetById(int id);
        Task<IDataResult<Cow>> GetByCowId(int cowId);
        Task<IResult> Add(Cow cow,int id , string securityKey);
        Task<IResult> Delete(Cow cow,int id , string securityKey);
        Task<IResult> Update(Cow cow,int id , string securityKey);
        Task<IDataResult<List<Cow>>> GetUserCows(int id, string securityKey);
    }
}