using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer.Repository.Impl
{
    public class FeatureRepository:GenericRepository<Feature>, IFeatureRepository
    {
        private readonly AppDbContext context;

        public FeatureRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<List<Feature>> GetFeaturesByCarId(int carId)
        {
            return await context.Features.Where(f=>f.CarId==carId).ToListAsync();
        }
    }
}
