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
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly AppDbContext context;

        public ImageRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<List<Image>> GetImagesByCarIdAsync(int carId)
        {
            return await context.Images
                 .Where(i => i.CarId == carId)
                 .ToListAsync();
        }

        public async Task<Image?> GetPrimaryImageByCarIdAsync(int carId)
        {
            return await context.Images
                .FirstOrDefaultAsync(i => i.CarId == carId && i.IsPrimary);
        }
    }
}
