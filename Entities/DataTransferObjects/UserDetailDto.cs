using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class UserDetailDto:IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public int? ZipCode { get; set; }
        public string ImagePath { get; set; }
        public double Profit { get; set; }
        public int TotalSales { get; set; }
        public int CustomerCount { get; set; }
        public int CowCount { get; set; }
        public int CalfCount { get; set; }
        public int SheepCount { get; set; }
        public int BullCount { get; set; }
        public int AnimalCount { get; set; }
        public int SuccessfulSales { get; set; }
        public int PendingOrders { get; set; }
        public int CanceledOrders { get; set; }
        public int ApprovedOrders { get; set; }
        public int DeliveryOrders { get; set; }
        public string Role { get; set; }
        
    }
}