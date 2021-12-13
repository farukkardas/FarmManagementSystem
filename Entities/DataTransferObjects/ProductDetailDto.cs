using System;
using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class ProductDetailDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public int SellerId { get; set; }
        public string ImagePath { get; set; }
        public string SellerName { get; set; }
        public string SellerLastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}