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
    public class BodyTypeRepository : GenericRepository<BodyType>, IBodyTypeRepository
    {
        private readonly AppDbContext context;

        public BodyTypeRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public async  Task<bool> ExistsAsync(string name)
        {
            return await context.BodyTypes.AnyAsync(bd => bd.Name == name);
        }
    }
}
