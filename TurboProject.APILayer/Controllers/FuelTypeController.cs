using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypeController : ControllerBase
    {
        private readonly IFuelTypeService fuelTypeService;

        public FuelTypeController(IFuelTypeService fuelTypeService)
        {
            this.fuelTypeService = fuelTypeService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllTypes()
        {
            var response = new ApiResponse<List<GetFuelTypeDto>>();
            var types = await fuelTypeService.GetAllFuelTypes();
            return Ok(types);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFuelType([FromBody] CreateFuelTypeDto createFuelTypeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await fuelTypeService.CreateFuelType(createFuelTypeDto);
            response.Success("Fuel type successfully created!");
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetFuelTypeDto>();
            var type = await fuelTypeService.GetFuelTypeById(id);
            if (type == null)
            {
                response.Error("Fuel type not found");
                return NotFound(response);
            }
            response.Success(type);
            return Ok(response);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            var response = new ApiResponse<string>();

            await fuelTypeService.DeleteFuelType(id);
            response.Success("fuel type successfully deleted");

            return Ok(response);
        }
    }
}
