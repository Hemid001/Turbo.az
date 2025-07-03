using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.City;
using TurboProject.BusinessLayer.Model.DTO.Response.City;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var response = new ApiResponse<List<GetCityDto>>();
            var city = await cityService.GetAllCities();
            return Ok(city);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetCityDto>();
            var city = await cityService.GetCityById(id);
            if (city == null)
            {
                response.Error("City not found");
                return NotFound(response);
            }
            response.Success(city);
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTransmissionType([FromBody] CreateCityDto createCityDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await cityService.CreateCity(createCityDto);
            response.Success("City successfully created!");
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var response = new ApiResponse<string>();

            await cityService.DeleteCity(id);
            response.Success("City successfully deleted");

            return Ok(response);
        }
    }
}
