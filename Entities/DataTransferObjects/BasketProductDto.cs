using Core.Entites.Abstract;

namespace Entities.DataTransferObjects
{
    public class BasketProductDto : IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}