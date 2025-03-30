using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.Transmission;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class TransmissionService:ITransmissionService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public TransmissionService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async  Task CreateTransmissionType(CreateTransmissionTypeDto createTransmissionTypeDto)
        {

            if (await unitofWork.transmissionRepository.ExistsAsync(createTransmissionTypeDto.TransmissionName))
                throw new Exception("Transmission type  with this name already exists");
            var type = mapper.Map<Transmission>(createTransmissionTypeDto);
            await unitofWork.transmissionRepository.Create(type);
            await unitofWork.Commit();
        }

        public async  Task DeleteTransmissionType(int id)
        {
            var type = await unitofWork.transmissionRepository.GetById(id);
            if (type == null)
                throw new KeyNotFoundException("Transmission type not found");
            unitofWork.transmissionRepository.Delete(type);
            await unitofWork.Commit();
        }

        public async  Task<List<GetTransmissionTypeDto>> GetAllTransmissionTypes()
        {
            var type = await unitofWork.transmissionRepository.GetAll();
            return mapper.Map<List<GetTransmissionTypeDto>>(type);
        }

        public async Task<GetTransmissionTypeDto> GetTransmissionTypeById(int id)
        {
            var type = await unitofWork.transmissionRepository.GetById(id);
            return mapper.Map<GetTransmissionTypeDto>(type);
        }
    }
}
