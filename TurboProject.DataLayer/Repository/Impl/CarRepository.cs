using Microsoft.EntityFrameworkCore;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Models.Car;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer.Repository.Impl
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private readonly AppDbContext context;

        public CarRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Car>> GetApprovedCars()
        {
            return await context.Cars
     .Where(c => c.IsApproved)
     .Include(c => c.Model)
         .ThenInclude(m => m.Brand)
     .Include(c => c.BodyType)
     .Include(c => c.City)
     .Include(c => c.EngineSize)
     .Include(c => c.FuelType)
     .Include(c => c.Transmission)
     .Include(c => c.Status)
     .Include(c => c.Favorites)
     .Include(c => c.Images)
     .Include(c => c.Features)
     .ToListAsync();

        }

        public async Task<List<Car>> GetFilteredCars(GetCarFilteredRequestModel model)
        {
            var query = context.Cars
                 .Include(c => c.Model)
                 .Include(c => c.FuelType)
                 .Include(c => c.Transmission)
                 .Include(c => c.City)
                 .AsQueryable();

            if (!string.IsNullOrEmpty(model.Model))
                query = query.Where(c => c.Model.Name == model.Model);

            if (!string.IsNullOrEmpty(model.FuelType))
                query = query.Where(c => c.FuelType.Name == model.FuelType);

            if (!string.IsNullOrEmpty(model.Transmission))
                query = query.Where(c => c.Transmission.Name == model.Transmission);

            if (!string.IsNullOrEmpty(model.City))
                query = query.Where(c => c.City.Name == model.City);

            if (!string.IsNullOrEmpty(model.CurrencyType))
                query = query.Where(c => c.CurrencyType.ToString() == model.CurrencyType);

            if (model.MinYear.HasValue)
                query = query.Where(c => c.Year >= model.MinYear.Value);

            if (model.MaxYear.HasValue)
                query = query.Where(c => c.Year <= model.MaxYear.Value);

            if (model.MinPrice.HasValue)
                query = query.Where(c => c.Price >= model.MinPrice.Value);

            if (model.MaxPrice.HasValue)
                query = query.Where(c => c.Price <= model.MaxPrice.Value);

            if (model.MinHP.HasValue)
                query = query.Where(c => c.HP >= model.MinHP.Value);

            if (model.MaxHP.HasValue)
                query = query.Where(c => c.HP <= model.MaxHP.Value);

            return await query.ToListAsync();

        }

        public async Task<Car> GetCarByIdWithIncludes(int id)
        {
            return await context.Cars
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)   // если нужна Brand у модели
                .Include(c => c.EngineSize)
                .Include(c => c.BodyType)
                .Include(c => c.FuelType)
                .Include(c => c.Transmission)
                .Include(c => c.City)
                .Include(c => c.Status)
                .Include(c => c.Favorites)
                .Include(c => c.Images)
                .Include(c => c.Features)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
