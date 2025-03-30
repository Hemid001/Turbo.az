using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Status;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IStatusService
    {
        Task<List<GetStatusDto>> GetAllStatuses();
        Task<GetStatusDto> GetById(int id);
        Task CreateStatus(CreateStatusDto dto);
        Task UpdateStatus(int id, UpdateStatusDto dto);
        Task DeleteStatus(int id);
    }
}
