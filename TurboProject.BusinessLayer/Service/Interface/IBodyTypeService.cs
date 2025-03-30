using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Response.BodyType;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IBodyTypeService
    {
        Task<List<GetBodyTypeDto>> GetAllTypes();
        Task<GetBodyTypeDto> GetById(int id);
        Task CreateBodyType(CreateBodyTypeDto dto);
        Task DeleteBodyType(int id);
    }
}
