using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Favorite;
using TurboProject.BusinessLayer.Model.DTO.Response.Favorite;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IFavoriteService
    {
        Task<List<GetFavoriteDto>> GetUserFavoritesAsync(int userId);
        Task AddToFavoritesAsync(int userId, CreateFavoriteDto dto);
        Task RemoveFromFavoritesAsync(int userId, int carId);
    }
}
