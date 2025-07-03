using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/admin")]
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

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = new ApiResponse<List<GetUserDto>>();

            var users = await userManager.Users.ToListAsync();
            var userDtos = new List<GetUserDto>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user); // Получаем роли пользователя
                var userDto = mapper.Map<GetUserDto>(user);
                userDto.Roles = roles.ToList(); // Заполняем список 
                userDtos.Add(userDto);
            }

            response.Success(userDtos);
            return Ok(response);
        }



        [HttpPost("users/{userId}/block")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            var response = new ApiResponse<string>();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Error("User not found");
                return NotFound(response);
            }
            await userManager.SetLockoutEnabledAsync(user,true);
            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));            
            await userManager.UpdateAsync(user);

            response.Success("User has been blocked.");
            return Ok(response);
        }

        [HttpPost("users/{userId}/unblock")]
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

        [HttpDelete("users/{userId}")]
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

        [HttpDelete("car/{carId}")]
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
    }
}
