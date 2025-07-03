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
    public class StatusRepository:GenericRepository<Status>,IStatusRepository
    {
        private readonly AppDbContext context;

        public StatusRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<Status> GetByNameAsync(string name)
        {
            return await context.Statuses.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
