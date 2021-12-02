using System;
using DataAccess.Entites;

namespace Entities.Concrete
{
    public class AnimalSales : IEntity
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double SalePrice { get; set; }
        public int AnimalType { get; set; }
        public int CustomerId { get; set; }
        public DateTime BoughtDate { get; set; }
        public int SellerId { get; set; }
    }
}