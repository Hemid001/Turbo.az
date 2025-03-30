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
    public class EngineSizeRepository : GenericRepository<EngineSize>, IEngineSizeRepository
    {
        private readonly AppDbContext context;

        public EngineSizeRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }
        public async Task<bool> ExistAsync(decimal size)
        {
            return await context.EngineSizes.AnyAsync(m => m.Engine == size);
        }
    }
}
