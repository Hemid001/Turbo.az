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
using TurboProject.BusinessLayer.Model.DTO.Response.Payment;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IAdminService
    {
        Task<List<GetStatusDto>> GetAllStatuses();
        Task<GetStatusDto> GetStatusById(int statusId);
        Task CreateStatus(CreateStatusDto dto);
        Task UpdateStatus(int statusId, UpdateStatusDto dto);
        Task DeleteStatus(int statusId);
        Task ApproveCar(int carId);
        Task RejectCar(int carId, string reason);
        Task DeleteCar(int carId);
        Task AssignRole( string userId, string role);
        Task RemoveRole( string userId, string role);
        Task CreateBodyType(CreateBodyTypeDto dto);
        Task DeleteBodyType(int id);
        Task CreateBrand(CreateBrandDto createBrandDto);
        Task UpdateBrand(UpdateBrandDto updateBrandDto);
        Task DeleteBrand(int id);
        Task CreateCity(CreateCityDto createCityDto);
        Task DeleteCity(int id);
        Task CreateEngineSize(CreateEngineSizeDto createDto);
        Task UpdateEngineSize(UpdateEngineSizeDto updateDto);
        Task DeleteEngineSize(int id);
        Task AddFeature(CreateFeatureDto dto);
        Task DeleteFeature(int id);
        Task CreateFuelType(CreateFuelTypeDto createFuelTypeDto);
        Task DeleteFuelType(int id);
        Task CreateModel(CreateModelRequestDto createModelRequestDto);
        Task UpdateModel(UpdateModelRequestDto updateModelRequestDto);
        Task DeleteModel(int id);
        Task CreateTransmissionType(CreateTransmissionTypeDto createTransmissionTypeDto);
        Task DeleteTransmissionType(int id);
    }
}
