using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.City;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.City;
using TurboProject.BusinessLayer.Model.DTO.Response.Transmission;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface ICityService
    {
        Task<List<GetCityDto>> GetAllCities();
        Task<GetCityDto> GetCityById(int id);
        Task CreateCity(CreateCityDto createCityDto);
        Task DeleteCity(int id);
    }
}
