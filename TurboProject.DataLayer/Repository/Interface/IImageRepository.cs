using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.DataLayer.Repository.Interface
{
    public interface IImageRepository:IGenericRepository<Image>
    {
        Task<List<Image>> GetImagesByCarIdAsync(int carId);
        Task<Image?> GetPrimaryImageByCarIdAsync(int carId);
    }
}
