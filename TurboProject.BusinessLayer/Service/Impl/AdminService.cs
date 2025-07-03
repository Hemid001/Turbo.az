using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class AdminService : IAdminService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ILogger logger;

        public AdminService(IUnitofWork unitofWork, IMapper mapper,
            UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }


        public async Task ApproveCar(int carId)
        {
            var car = await unitofWork.carRepository.GetById(carId);
            if (car == null)
                throw new KeyNotFoundException("Car not found");

            car.IsApproved = true;
            unitofWork.carRepository.Update(car);
            await unitofWork.Commit();

        }

        public async Task RejectCar(int carId, string reason)
        {
            var car = await unitofWork.carRepository.GetById(carId);
            if (car == null)
                throw new KeyNotFoundException("Car not found");

            car.IsApproved = false;
            car.RejectionReason = reason;
            unitofWork.carRepository.Update(car);
            await unitofWork.Commit();

        }

        public async Task DeleteCar(int carId)
        {
            var car = await unitofWork.carRepository.GetById(carId);
            if (car == null)
                throw new KeyNotFoundException("Car not found");

            unitofWork.carRepository.Delete(car);
            await unitofWork.Commit();

        }

        public async Task AssignRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (!await roleManager.RoleExistsAsync(role))
                throw new ArgumentException($"Role '{role}' does not exist");

            await userManager.AddToRoleAsync(user, role);
        }

        public async Task RemoveRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            await userManager.RemoveFromRoleAsync(user, role);
        }


    }
}
