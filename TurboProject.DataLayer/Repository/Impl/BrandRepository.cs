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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly AppDbContext context;

        public BrandRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }
        public async Task<bool> ExistAsync(string name)
        {
            return await context.Brands.AnyAsync(b => b.Name == name);
        }
    }
}
