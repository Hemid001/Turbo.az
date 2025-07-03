using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Response.Features;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IFeatureService
    {
        Task<List<GetFeatureDto>> GetFeaturesByCarId(int carId);
        Task AddFeature(CreateFeatureDto dto);
        Task DeleteFeature(int id);

    }
}
