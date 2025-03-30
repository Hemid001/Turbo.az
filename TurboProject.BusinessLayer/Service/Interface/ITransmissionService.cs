using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.Transmission;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface ITransmissionService
    {
        Task<List<GetTransmissionTypeDto>> GetAllTransmissionTypes();
        Task<GetTransmissionTypeDto> GetTransmissionTypeById(int id);
        Task CreateTransmissionType(CreateTransmissionTypeDto createTransmissionTypeDto);
        Task DeleteTransmissionType(int id);
    }
}
