using Microsoft.AspNetCore.Http;

namespace TurboProject.BusinessLayer.Model.DTO.Request.Image
{
    public class CreateImageDto
    {
        public int CarId { get; set; }
        public IFormFile Image { get; set; }
        public bool IsPrimary { get; set; }
    }
}
