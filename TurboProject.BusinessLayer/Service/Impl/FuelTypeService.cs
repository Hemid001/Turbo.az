using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public FuelTypeService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
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

        public async Task<List<GetFuelTypeDto>> GetAllFuelTypes()
        {
            var type = await unitofWork.fuelTypeRepository.GetAll();
            return mapper.Map<List<GetFuelTypeDto>>(type);
        }

        public async Task<GetFuelTypeDto> GetFuelTypeById(int id)
        {
            var type = await unitofWork.fuelTypeRepository.GetById(id);
            return mapper.Map<GetFuelTypeDto>(type);
        }
    }
}
