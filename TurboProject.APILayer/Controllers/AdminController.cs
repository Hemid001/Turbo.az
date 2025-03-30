using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Request.City;
using TurboProject.BusinessLayer.Model.DTO.Request.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Status;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;
using TurboProject.BusinessLayer.Model.DTO.Response.User;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IAdminService adminService;

        public AdminController(UserManager<User> userManager, IMapper mapper, IAdminService adminService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.adminService = adminService;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = new ApiResponse<List<GetUserDto>>();

            var users = await userManager.Users.ToListAsync();
            var userDtos = new List<GetUserDto>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user); // Получаем роли пользователя
                var userDto = mapper.Map<GetUserDto>(user);
                userDto.Roles = roles.ToList(); // Заполняем список ролей

                userDtos.Add(userDto);
            }

            response.Success(userDtos);
            return Ok(response);
        }



        [HttpPost("BlockUser/{userId}")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            var response = new ApiResponse<string>();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Error("User not found");
                return NotFound(response);
            }

            user.LockoutEnd = DateTime.UtcNow.AddYears(100);
            await userManager.UpdateAsync(user);

            response.Success("User has been blocked.");
            return Ok(response);
        }

        [HttpPost("UnblockUser/{userId}")]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            var response = new ApiResponse<string>();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Error("User not found");
                return NotFound(response);
            }

            user.LockoutEnd = null;
            await userManager.UpdateAsync(user);

            response.Success("User has been unblocked.");
            return Ok(response);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var response = new ApiResponse<string>();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Error("User not found");
                return NotFound(response);
            }

            await userManager.DeleteAsync(user);

            response.Success("User has been deleted.");
            return Ok(response);
        }

        [HttpGet("All Statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var response = new ApiResponse<List<GetStatusDto>>();
            var statuses = await adminService.GetAllStatuses();
            response.Success(statuses);
            return Ok(response);
        }

        [HttpGet("Status/{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var response = new ApiResponse<GetStatusDto>();
            var status = await adminService.GetStatusById(id);
            if (status != null)
            {
                response.Error("Status not found");
                return NotFound(response);
            }
            response.Success(status);
            return Ok(response);
        }

        [HttpPost("Create status")]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            await adminService.CreateStatus(dto);
            response.Success("Status created successfully.");
            return Ok(response);
        }

        [HttpPatch("Status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            await adminService.UpdateStatus(id, dto);
            response.Success("Status updated successfully.");
            return Ok(response);
        }

        [HttpDelete("DeleteStatus/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteStatus(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteStatus(id);
            response.Success("Status deleted successfully.");
            return Ok(response);
        }

        [HttpPost("approve-car/{carId}")]
        public async Task<IActionResult> ApproveCar(int carId)
        {
            var response = new ApiResponse<string>();
            response.Success("Car approved successfully");
            await adminService.ApproveCar(carId);
            return Ok(response);
        }

        [HttpPost("reject-car/{carId}")]
        public async Task<IActionResult> RejectCar(int carId, [FromBody] string reason)
        {
            var response = new ApiResponse<string>();
            response.Success("Car rejected successfully");
            await adminService.RejectCar(carId, reason);
            return Ok(response);
        }

        [HttpDelete("delete-car/{carId}")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            var response = new ApiResponse<string>();
            response.Success("Car deleted successfully");
            await adminService.DeleteCar(carId);
            return Ok(response);
        }
        [HttpPost("assign-role/{userId}")]
        public async Task<IActionResult> AssignRole(string userId, [FromBody] string role)
        {
            var response = new ApiResponse<string>();
            response.Success("Role assigned successfully");

            // Вызываем сервис для назначения роли
            await adminService.AssignRole(userId, role);
            return Ok(response);
        }

        [HttpPost("remove-role/{userId}")]
        public async Task<IActionResult> RemoveRole(string userId, [FromBody] string role)
        {
            var response = new ApiResponse<string>();
            response.Success("Role removed successfully");

            // Вызываем сервис для удаления роли
            await adminService.RemoveRole(userId, role);
            return Ok(response);
        }


        [HttpPost("Create bodytype")]
        public async Task<IActionResult> CreateBodyType([FromBody] CreateBodyTypeDto createBodyTypeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateBodyType(createBodyTypeDto);
            response.Success("Body type successfully created!");
            return Ok(response);
        }

        [HttpDelete("DeleteBodyType/{id}")]
        public async Task<IActionResult> DeleteBodyType(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteBodyType(id);
            response.Success("Body type successfully deleted");

            return Ok(response);
        }

        [HttpPost("Create brand")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto createBrandDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateBrand(createBrandDto);
            response.Success("Brand successfully created!");
            return Ok(response);
        }

        [HttpPut("Update brand")]
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
                await adminService.UpdateBrand(updateBrandDto);
                response.Success("Brand successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }

        [HttpDelete("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteBrand(id);
            response.Success("Brand successfully deleted");

            return Ok(response);
        }

        [HttpPost("Create city")]
        public async Task<IActionResult> CreateTransmissionType([FromBody] CreateCityDto createCityDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateCity(createCityDto);
            response.Success("City successfully created!");
            return Ok(response);
        }

        [HttpDelete("DeleteCity/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteCity(id);
            response.Success("City successfully deleted");

            return Ok(response);
        }

        [HttpPost("Create engine size")]
        public async Task<IActionResult> CreateEngineSize([FromBody] CreateEngineSizeDto createEngineSizeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateEngineSize(createEngineSizeDto);
            response.Success("Engine size successfully created!");
            return Ok(response);
        }

        [HttpPut("Update engine size")]
        public async Task<IActionResult> UpdateEngineSize([FromBody] UpdateEngineSizeDto updateEngineSizeDto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            try
            {
                await adminService.UpdateEngineSize(updateEngineSizeDto);
                response.Success("Size successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }
        [HttpDelete("DeleteEngineSize/{id}")]
        public async Task<IActionResult> DeleteEngineSize(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteEngineSize(id);
            response.Success("Engine size successfully deleted");

            return Ok(response);
        }
        [HttpPost("Create feature")]
        public async Task<IActionResult> AddFeature([FromBody] CreateFeatureDto dto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            await adminService.AddFeature(dto);
            response.Success("Feature added successfully.");
            return Ok(response);
        }

        [HttpDelete("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            await adminService.DeleteFeature(id);
            return Ok();
        }

        [HttpPost("Create fuel type")]
        public async Task<IActionResult> CreateFuelType([FromBody] CreateFuelTypeDto createFuelTypeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateFuelType(createFuelTypeDto);
            response.Success("Fuel type successfully created!");
            return Ok(response);
        }

        [HttpDelete("DeleteFuelType/{id}")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteFuelType(id);
            response.Success("fuel type successfully deleted");

            return Ok(response);
        }

        [HttpPost("Create model")]
        public async Task<IActionResult> Create([FromBody] CreateModelRequestDto dto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateModel(dto);
            response.Success("Model successfully created!");
            return Ok(response);
        }

        [HttpPut("Update model")]
        public async Task<IActionResult> UpdateModel([FromBody] UpdateModelRequestDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            try
            {
                await adminService.UpdateModel(dto);
                response.Success("Model successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }

        [HttpDelete("DeleteModel/{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteModel(id);
            response.Success("Car successfully deleted");

            return Ok(response);
        }

        [HttpPost("Create transmission type")]
        public async Task<IActionResult> CreateTransmissionType([FromBody] CreateTransmissionTypeDto createTransmissionTypeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await adminService.CreateTransmissionType(createTransmissionTypeDto);
            response.Success("Transmission type successfully created!");
            return Ok(response);
        }

        [HttpDelete("DeleteTransmissionType/{id}")]
        public async Task<IActionResult> DeleteTransmissionType(int id)
        {
            var response = new ApiResponse<string>();

            await adminService.DeleteTransmissionType(id);
            response.Success("Transmission type successfully deleted");

            return Ok(response);
        }
    }
}
