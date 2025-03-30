using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.CarsModel;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = new ApiResponse<List<GetBrandDto>>();
            var brands = await brandService.GetAllBrands();
            return Ok(brands);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto createBrandDto)
        {
            var response =new ApiResponse<string>();
            if(!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await brandService.CreateBrand(createBrandDto);
            response.Success("Brand successfully created!");
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetBrandDto>();
            var brand = await brandService.GetBrandById(id);
            if (brand == null)
            {
                response.Error("Brand not found");
                return NotFound(response);
            }
            response.Success(brand);
            return Ok(response);
        }
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandDto updateBrandDto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            try
            {
                await brandService.UpdateBrand(updateBrandDto);
                response.Success("Brand successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var response = new ApiResponse<string>();

            await brandService.DeleteBrand(id);
            response.Success("Brand successfully deleted");

            return Ok(response);
        }
    }
}
