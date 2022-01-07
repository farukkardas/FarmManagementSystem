using System;
using DataAccess.Entites;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ProductType { get; set; }
        public double Price { get; set; }
        public int DeliveryCity { get; set; }
        public string DeliveryDistrict { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime BoughtDate { get; set; }
        public int Status { get; set; }
        public int? DeliveryNo { get; set; }
    }
}