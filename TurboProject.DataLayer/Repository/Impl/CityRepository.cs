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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly AppDbContext context;

        public CityRepository(AppDbContext  context):base(context) 
        {
            this.context = context;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await context.Cities.AnyAsync(ft => ft.Name == name);
        }
    }
}
