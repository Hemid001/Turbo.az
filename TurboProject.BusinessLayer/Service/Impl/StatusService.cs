using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Status;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class StatusService : IStatusService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public StatusService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async  Task CreateStatus(CreateStatusDto dto)
        {
            var existingStatus = await unitofWork.statusRepository.GetByNameAsync(dto.StatusName);
            if (existingStatus != null)
                throw new InvalidOperationException("Status already exists");

            var status = mapper.Map<Status>(dto);
            status.EndTime = DateTime.UtcNow.AddDays(dto.DurationDays);

            await unitofWork.statusRepository.Create(status);
        }

        public async Task DeleteStatus(int id)
        {
            var status = await unitofWork.statusRepository.GetById(id);
            if (status == null)
                throw new KeyNotFoundException("Status not found");

             unitofWork.statusRepository.Delete(status);
        }

        public async  Task<List<GetStatusDto>> GetAllStatuses()
        {
            var statuses = await unitofWork.statusRepository.GetAll();
            return mapper.Map<List<GetStatusDto>>(statuses);
        }

        public async Task<GetStatusDto> GetById(int id)
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
        }
    }
}
