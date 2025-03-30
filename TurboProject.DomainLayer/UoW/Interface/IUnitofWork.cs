using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DomainLayer.UoW.Interface
{
    public interface IUnitofWork
    {
        IBrandRepository brandRepository {  get; }
        ICarRepository carRepository { get; }
        IModelRepository modelRepository { get; }
        IEngineSizeRepository engineSizeRepository { get; }
        IFuelTypeRepository fuelTypeRepository { get; }
        ITransmissionRepository transmissionRepository { get; }
        ICityRepository cityRepository { get; }
        IImageRepository imageRepository { get; }
        IBodyTypeRepository bodyTypeRepository { get; }
        IFavoriteRepository favoriteRepository { get; }
        IPaymentRepository paymentRepository { get; }
        IStatusRepository statusRepository { get; }
        IFeatureRepository featureRepository { get; }




        Task<int> Commit();
    }
}
