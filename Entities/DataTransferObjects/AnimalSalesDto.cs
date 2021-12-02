using System;
using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class AnimalSalesDto : IDto
    {
        public int SalesId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Amount { get; set; }
        public double SalePrice { get; set; }
        public int AnimalType { get; set; }
        public int SellerId { get; set; }
        public DateTime BoughtDate { get; set; }
    }
}