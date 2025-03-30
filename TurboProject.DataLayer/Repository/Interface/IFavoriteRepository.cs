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
        Task<bool> ExistsAsync(int userId, int carId);
        Task<List<Favorite>> GetUserFavorites(int userId);
        Task<Favorite> GetByUserAndCar(int userId, int carId);
    }
}
