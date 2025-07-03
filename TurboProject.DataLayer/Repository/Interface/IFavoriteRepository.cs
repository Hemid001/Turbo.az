using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.DataLayer.Repository.Interface
{
    public interface IFavoriteRepository:IGenericRepository<Favorite>
    {
        Task<bool> ExistsAsync(string userId, int carId);
        Task<List<Favorite>> GetUserFavorites(string userId);
        Task<Favorite> GetByUserAndCar(string userId, int carId);
    }
}
