using Microsoft.EntityFrameworkCore;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer.Repository.Impl
{
    public class ModelRepository : GenericRepository<CarsModel>, IModelRepository
    {
        private readonly AppDbContext _context;

        public ModelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Models.AnyAsync(m => m.Name == name);
        }

    }

}
