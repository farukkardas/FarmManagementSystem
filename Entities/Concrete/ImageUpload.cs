using DataAccess.Entites;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{
    public class ImageUpload : IEntity
    {
        public IFormFile Image { get; set; }
    }
}