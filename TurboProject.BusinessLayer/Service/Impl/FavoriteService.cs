using AutoMapper;
using TurboProject.BusinessLayer.Model.DTO.Request.Favorite;
using TurboProject.BusinessLayer.Model.DTO.Response.Favorite;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Impl;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public FavoriteService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async Task AddToFavoritesAsync(string userId, CreateFavoriteDto dto)
        {
            if (await unitofWork.favoriteRepository.ExistsAsync(userId, dto.CarId))
                throw new InvalidOperationException("Car is already in favorites.");

            var favorite = mapper.Map<Favorite>(dto);
            favorite.UserId = userId;

            await unitofWork.favoriteRepository.Create(favorite);
            await unitofWork.Commit();
        }

        public async Task<List<GetFavoriteDto>> GetUserFavoritesAsync(string userId)
        {
            var favorites = await unitofWork.favoriteRepository.GetUserFavorites(userId);
            return mapper.Map<List<GetFavoriteDto>>(favorites);
        }

        public async Task RemoveFromFavoritesAsync(string userId, int carId)
        {
            var favorite = await unitofWork.favoriteRepository.GetByUserAndCar(userId, carId);
            if (favorite == null)
                throw new KeyNotFoundException("Favorite not found");

            unitofWork.favoriteRepository.Delete(favorite);
            await unitofWork.Commit();
            
        }
    }
}
