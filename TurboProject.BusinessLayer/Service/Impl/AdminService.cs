using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Impl;
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

        public AdminService(IUnitofWork unitofWork,IMapper mapper,
            UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }
        public async Task CreateStatus(CreateStatusDto dto)
        {
            var existingStatus = await unitofWork.statusRepository.GetByNameAsync(dto.StatusName);
            if (existingStatus != null)
                throw new InvalidOperationException("Status already exists");

            var status = mapper.Map<Status>(dto);
            status.EndTime = DateTime.UtcNow.AddDays(dto.DurationDays);

            await unitofWork.statusRepository.Create(status);
            await unitofWork.Commit();
        }

        public async Task DeleteStatus(int id)
        {
            var status = await unitofWork.statusRepository.GetById(id);
            if (status == null)
                throw new KeyNotFoundException("Status not found");

            unitofWork.statusRepository.Delete(status);
            await unitofWork.Commit();
        }

        public async Task<List<GetStatusDto>> GetAllStatuses()
        {
            var statuses = await unitofWork.statusRepository.GetAll();
            return mapper.Map<List<GetStatusDto>>(statuses);
        }

        public async Task<GetStatusDto> GetStatusById(int id)
        {
            var status = await unitofWork.statusRepository.GetById(id);
            if (status == null)
                throw new KeyNotFoundException("Status not found");

            return mapper.Map<GetStatusDto>(status);
        }

        public async Task UpdateStatus(int id, UpdateStatusDto dto)
        {
            var status = await unitofWork.statusRepository.GetById(id);
            if (status == null)
                throw new KeyNotFoundException("Status not found");

            status.StatusName = dto.StatusName;
            status.EndTime = DateTime.UtcNow.AddDays(dto.DurationDays);

            unitofWork.statusRepository.Update(status);
            await unitofWork.Commit();

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


        public async Task CreateBodyType(CreateBodyTypeDto dto)
        {
            if (await unitofWork.bodyTypeRepository.ExistsAsync(dto.BodyTypeName))
                throw new Exception("Body type  with this name already exists");
            var type = mapper.Map<BodyType>(dto);
            await unitofWork.bodyTypeRepository.Create(type);
            await unitofWork.Commit();
        }

        public async Task DeleteBodyType(int id)
        {
            var type = await unitofWork.bodyTypeRepository.GetById(id);
            if (type == null)
                throw new KeyNotFoundException("Body type not found");
            unitofWork.bodyTypeRepository.Delete(type);
            await unitofWork.Commit();
        }

        public async Task CreateBrand(CreateBrandDto createBrandDto)
        {
            if (await unitofWork.brandRepository.ExistAsync(createBrandDto.Name))
                throw new Exception("Brand with this name already exists");
            var brand = mapper.Map<Brand>(createBrandDto);
            await unitofWork.brandRepository.Create(brand);
            await unitofWork.Commit();
        }

        public async Task DeleteBrand(int id)
        {
            var brand = await unitofWork.brandRepository.GetById(id);
            if (brand == null)
                throw new KeyNotFoundException("Brand not found");
            unitofWork.brandRepository.Delete(brand);
            await unitofWork.Commit();
        }

        public async Task UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var brand = await unitofWork.brandRepository.GetById(updateBrandDto.Id);
            if (brand == null)
                throw new Exception("Brand not found");
            mapper.Map(updateBrandDto, brand);
            unitofWork.brandRepository.Update(brand);
            await unitofWork.Commit();
        }
        public async Task CreateCity(CreateCityDto createCityDto)
        {

            if (await unitofWork.cityRepository.ExistsAsync(createCityDto.CityName))
                throw new Exception("City  already exists");
            var city = mapper.Map<City>(createCityDto);
            await unitofWork.cityRepository.Create(city);
            await unitofWork.Commit();
        }

        public async Task DeleteCity(int id)
        {
            var city = await unitofWork.cityRepository.GetById(id);
            if (city == null)
                throw new KeyNotFoundException("City not found");
            unitofWork.cityRepository.Delete(city);
            await unitofWork.Commit();
        }
        public async Task CreateEngineSize(CreateEngineSizeDto createDto)
        {
            if (await unitofWork.engineSizeRepository.ExistAsync(createDto.Engine))
                throw new Exception("EngineSize with this size already exists");
            var engineSize = mapper.Map<EngineSize>(createDto);
            await unitofWork.engineSizeRepository.Create(engineSize);
            await unitofWork.Commit();
        }

        public async Task DeleteEngineSize(int id)
        {
            var size = await unitofWork.engineSizeRepository.GetById(id);
            if (size == null)
                throw new KeyNotFoundException("Engine size not found");
            unitofWork.engineSizeRepository.Delete(size);
            await unitofWork.Commit();
        }
        public async Task UpdateEngineSize(UpdateEngineSizeDto updateDto)
        {
            var size = await unitofWork.engineSizeRepository.GetById(updateDto.Id);
            if (size == null)
                throw new Exception("Engine size not found");
            mapper.Map(updateDto, size);
            unitofWork.engineSizeRepository.Update(size);
            await unitofWork.Commit();
        }
        public async Task AddFeature(CreateFeatureDto dto)
        {
            var feature = mapper.Map<Feature>(dto);
            await unitofWork.featureRepository.Create(feature);
            await unitofWork.Commit();

        }

        public async Task DeleteFeature(int id)
        {
            var feature = await unitofWork.featureRepository.GetById(id);
            if (feature != null)
            {
                unitofWork.featureRepository.Delete(feature);
            }
            await unitofWork.Commit();
        }
        public async Task CreateFuelType(CreateFuelTypeDto createFuelTypeDto)
        {
            if (await unitofWork.fuelTypeRepository.ExistsAsync(createFuelTypeDto.FuelTypeName))
                throw new Exception("Fuel type  with this name already exists");
            var type = mapper.Map<FuelType>(createFuelTypeDto);
            await unitofWork.fuelTypeRepository.Create(type);
            await unitofWork.Commit();
        }

        public async Task DeleteFuelType(int id)
        {
            var type = await unitofWork.fuelTypeRepository.GetById(id);
            if (type == null)
                throw new KeyNotFoundException("Fuel type not found");
            unitofWork.fuelTypeRepository.Delete(type);
            await unitofWork.Commit();
        }
        public async Task CreateModel(CreateModelRequestDto createModelRequestDto)
        {
            var model = mapper.Map<CarsModel>(createModelRequestDto);
            await unitofWork.modelRepository.Create(model);
            await unitofWork.Commit();
        }

        public async Task DeleteModel(int id)
        {
            var model = await unitofWork.modelRepository.GetById(id);
            if (model == null)
                throw new KeyNotFoundException("Model not found");

            unitofWork.modelRepository.Delete(model);
            await unitofWork.Commit();
        }
        public async Task UpdateModel(UpdateModelRequestDto updateModelRequestDto)
        {
            var model = await unitofWork.modelRepository.GetById(updateModelRequestDto.Id);
            if (model == null)
                throw new Exception("Model not found");
            mapper.Map(updateModelRequestDto, model);
            unitofWork.modelRepository.Update(model);
            await unitofWork.Commit();
        }
        public async Task CreateTransmissionType(CreateTransmissionTypeDto createTransmissionTypeDto)
        {

            if (await unitofWork.transmissionRepository.ExistsAsync(createTransmissionTypeDto.TransmissionName))
                throw new Exception("Transmission type  with this name already exists");
            var type = mapper.Map<Transmission>(createTransmissionTypeDto);
            await unitofWork.transmissionRepository.Create(type);
            await unitofWork.Commit();
        }

        public async Task DeleteTransmissionType(int id)
        {
            var type = await unitofWork.transmissionRepository.GetById(id);
            if (type == null)
                throw new KeyNotFoundException("Transmission type not found");
            unitofWork.transmissionRepository.Delete(type);
            await unitofWork.Commit();
        }

    }
}
