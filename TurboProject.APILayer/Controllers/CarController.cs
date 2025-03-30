using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Car;
using TurboProject.BusinessLayer.Model.DTO.Response.Car;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarRequestDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                response.Error("User ID not found in token");
                return Unauthorized(response);
            }

            await carService.CreateCar(dto);
            response.Success("Car successfully created");
            return Ok(response);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateCar([FromBody] UpdateCarRequestDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            try
            {
                await carService.UpdateCar(dto);
                response.Success("Car successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var response = new ApiResponse<string>();

            await carService.DeleteCar(id);
            response.Success("Car successfully deleted");

            return Ok(response);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = new ApiResponse<GetCarResponseDto>();

            var car = await carService.GetCarById(id);
            if (car == null)
            {
                response.Error("Car not found");
                return NotFound(response);
            }

            response.Success(car);
            return Ok(response);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllCars()
        {
            var response = new ApiResponse<List<GetCarResponseDto>>();

            var cars = await carService.GetAllCars();
            response.Success(cars);

            return Ok(response);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetFilteredCars([FromQuery] GetCarFilteredRequestDto filterDto)
        {
            var response = new ApiResponse<List<GetCarResponseDto>>();

            var cars = await carService.GetFilteredCars(filterDto);
            response.Success(cars);

            return Ok(response);
        }

    }
}
