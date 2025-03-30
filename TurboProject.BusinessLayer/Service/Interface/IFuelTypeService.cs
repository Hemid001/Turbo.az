using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IFuelTypeService
    {
        Task<List<GetFuelTypeDto>> GetAllFuelTypes();
        Task<GetFuelTypeDto> GetFuelTypeById(int id);
        Task CreateFuelType(CreateFuelTypeDto createFuelTypeDto);
        Task DeleteFuelType(int id);
    }
}
