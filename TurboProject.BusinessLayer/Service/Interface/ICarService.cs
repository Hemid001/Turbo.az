using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Car;
using TurboProject.BusinessLayer.Model.DTO.Response.Car;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface ICarService
    {
        Task CreateCar(CreateCarRequestDto model);
        Task<List<GetCarResponseDto>> GetAllCars();
        Task<GetCarResponseDto> GetCarById(int id);
        Task<List<GetCarResponseDto>> GetFilteredCars(GetCarFilteredRequestDto filter);
        Task UpdateCar(UpdateCarRequestDto model);
        Task DeleteCar(int id);

    }
}
