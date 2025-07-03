using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.City;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.City;
using TurboProject.BusinessLayer.Model.DTO.Response.Transmission;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class CityService : ICityService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public CityService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async  Task<List<GetCityDto>> GetAllCities()
        {
            var city = await unitofWork.cityRepository.GetAll();
            return mapper.Map<List<GetCityDto>>(city);
        }

        public async  Task<GetCityDto> GetCityById(int id)
        {
            var city = await unitofWork.cityRepository.GetById(id);
            return mapper.Map<GetCityDto>(city);
        }
        public async Task CreateCity(CreateCityDto createCityDto)
        {

            if (await unitofWork.cityRepository.ExistsAsync(createCityDto.Name))
                throw new Exception("City  already exists");
            var city = mapper.Map<City>(createCityDto);
            await unitofWork.cityRepository.Create(city);
            await unitofWork.Commit();
        }

        public async Task DeleteCity(int id)
        {
            var city = await unitofWork.cityRepository.GetById(id);
            if (city == null)
                throw new KeyNotFoundException("City not found");
            unitofWork.cityRepository.Delete(city);
            await unitofWork.Commit();
        }
    }
}
