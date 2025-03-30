using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Response.CarsModel;
namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface ICarsModelService
    {
        Task<List<GetModelResponseDto>> GetAllModels();
        Task<GetModelResponseDto> GetModelById(int id);
        Task CreateModel(CreateModelRequestDto createModelRequestDto);
        Task UpdateModel(UpdateModelRequestDto updateModelRequestDto);
        Task DeleteModel(int id);
    }
}
