using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Request.City;
using TurboProject.BusinessLayer.Model.DTO.Request.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Status;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.Payment;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IAdminService
    {
        Task ApproveCar(int carId);
        Task RejectCar(int carId, string reason);
        Task DeleteCar(int carId);
        Task AssignRole( string userId, string role);
        Task RemoveRole( string userId, string role);
 
    }
}
