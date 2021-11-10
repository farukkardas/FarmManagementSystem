using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class UserDetailDto:IDto
    {
        public int Id { get; set; }
        public double Profit { get; set; }
        public int TotalSales { get; set; }
        public int CustomerCount { get; set; }
        public int AnimalCount { get; set; }
        
    }
}