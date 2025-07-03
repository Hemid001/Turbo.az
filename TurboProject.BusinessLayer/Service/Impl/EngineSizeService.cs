using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.EngineSize;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class EngineSizeService : IEngineSizeService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public EngineSizeService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async  Task<List<GetEngineSizeDto>> GetAllEngineSizes()
        {
            var size = await unitofWork.engineSizeRepository.GetAll();
            return mapper.Map<List<GetEngineSizeDto>>(size);
        }

        public async Task<GetEngineSizeDto> GetEngineSizeById(int id)
        {
            var size = await unitofWork.engineSizeRepository.GetById(id);
            return mapper.Map<GetEngineSizeDto>(size);
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
    }
}
