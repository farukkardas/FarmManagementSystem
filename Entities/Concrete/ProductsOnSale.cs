using System;
using DataAccess.Entites;

namespace Entities.Concrete
{
    public class ProductsOnSale : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime EntryDate { get; set; }
        public int SellerId { get; set; }
        public string ImagePath { get; set; }
    }
}