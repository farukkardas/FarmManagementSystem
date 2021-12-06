﻿using System;
using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class OrderDetailDto : IDto
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string CustomerName { get; set; }
        public int ProductType { get; set; }
        public string ProductName { get; set; }
        public int DeliveryCity { get; set; }
        public string DeliveryDistrict { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool Status { get; set; }
    }
}