using Microsoft.EntityFrameworkCore;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer.Repository.Impl
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly AppDbContext context;

        public FavoriteRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int carId)
        {
            return await context.Favorites.AnyAsync(f => f.UserId == userId && f.CarId == carId);
        }

        public async Task <Favorite> GetByUserAndCar(int userId, int carId)
        {
            return await context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.CarId == carId);

        }

        public async Task<List<Favorite>> GetUserFavorites(int userId)
        {
            return await context.Favorites
               .Where(f => f.UserId == userId)
               .Include(f => f.Car)
               .ToListAsync();
        }
    }
}
