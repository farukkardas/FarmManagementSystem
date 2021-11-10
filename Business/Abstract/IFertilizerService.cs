using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFertilizerService
    {
        IDataResult<List<Fertilizer>> GetAll();
        IDataResult<Fertilizer> GetById(int id);
        
        IResult Add(Fertilizer cow);
        IResult Delete(Fertilizer cow);
        IResult Update(Fertilizer cow);
        IDataResult<List<Fertilizer>> GetUserFertilizers(int id, string securityKey);
    }
}