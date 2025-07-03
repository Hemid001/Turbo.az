using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IBrandService
    {
        Task<List<GetBrandDto>> GetAllBrands();
        Task<GetBrandDto> GetBrandById(int id);
        Task CreateBrand(CreateBrandDto createBrandDto);
        Task UpdateBrand(UpdateBrandDto updateBrandDto);
        Task DeleteBrand(int id);
    }
}
