using System;
using Core.Entites.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.DataTransferObjects
{
    public class MilkSalesDto : IDto
    {
        public int SalesId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public int SellerId { get; set; }
        public DateTime BoughtDate { get; set; }
    }
}