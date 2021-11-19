using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFertilizerService
    {
        IDataResult<List<Fertilizer>> GetAll();
        IDataResult<Fertilizer> GetById(int id);
        
        IResult Add(Fertilizer cow,int id,string securityKey);
        IResult Delete(Fertilizer cow,int id,string securityKey);
        IResult Update(Fertilizer cow,int id,string securityKey);
        IDataResult<List<Fertilizer>> GetUserFertilizers(int id, string securityKey);
    }
}