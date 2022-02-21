using DataAccess.Entites;
using Microsoft.AspNetCore.Http;

namespace Core.Entities.Concrete
{
    public class ImageUpload : IEntity
    {
        public IFormFile File { get; set; }
    }
}