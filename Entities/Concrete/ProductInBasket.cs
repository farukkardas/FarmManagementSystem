using DataAccess.Entites;

namespace Entities.Concrete
{
    public class ProductInBasket : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}