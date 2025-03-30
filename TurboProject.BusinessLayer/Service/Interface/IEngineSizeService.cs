using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Response.EngineSize;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IEngineSizeService
    {
        Task CreateEngineSize(CreateEngineSizeDto createDto);
        Task UpdateEngineSize(UpdateEngineSizeDto updateDto);
        Task DeleteEngineSize(int id);
        Task<List<GetEngineSizeDto>> GetAllEngineSizes();
        Task<GetEngineSizeDto> GetEngineSizeById(int id);
    }
}
