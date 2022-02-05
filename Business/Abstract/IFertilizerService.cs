using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFertilizerService
    {
        Task<IDataResult<List<Fertilizer>>> GetAll();
        Task<IDataResult<Fertilizer>> GetById(int id);
        Task<IResult> Add(Fertilizer cow,int id,string securityKey);
        Task<IResult> Delete(Fertilizer cow,int id,string securityKey);
        Task<IResult> Update(Fertilizer cow,int id,string securityKey);
        Task<IDataResult<List<Fertilizer>>> GetUserFertilizers(int id, string securityKey);
    }
}