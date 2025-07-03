using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Models.Car;

namespace TurboProject.DataLayer.Repository.Interface
{
    public interface ICarRepository:IGenericRepository<Car>
    {
        Task<List<Car>> GetFilteredCars(GetCarFilteredRequestModel model);
        Task<List<Car>> GetApprovedCars();
        Task<Car> GetCarByIdWithIncludes(int id);
    }
}
