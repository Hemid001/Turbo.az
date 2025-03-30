using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Response.Image;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadImage(int carId, IFormFile file, bool isPrimary)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await imageService.UploadImage(carId, file, isPrimary);
            return Ok(response);
        }

        [HttpGet("car/{carId}")]
        public async Task<IActionResult> GetImagesByCar(int carId)
        {
            var response = new ApiResponse<GetImageDto>();
            var images = await imageService.GetImagesByCarId(carId);
            if (images == null)
            {
                response.Error("Images not found");
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("car/{carId}/primary")]
        public async Task<IActionResult> GetPrimaryImageByCar(int carId)
        {
            var response = new ApiResponse<GetImageDto>();
            var image = await imageService.GetPrimaryImageByCarId(carId);
            if (image == null)
            {
                response.Error("No primary image found");
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
