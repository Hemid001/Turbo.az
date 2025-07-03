using AutoMapper;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Response.CarsModel;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class CarsModelService : ICarsModelService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public CarsModelService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async Task<List<GetModelResponseDto>> GetAllModels()
        {
            var models = await unitofWork.modelRepository.GetAll();
            return mapper.Map<List<GetModelResponseDto>>(models);
        }

        public async Task<GetModelResponseDto> GetModelById(int id)
        {
            var model = await unitofWork.modelRepository.GetById(id);
            return mapper.Map<GetModelResponseDto>(model);
        }
        public async Task CreateModel(CreateModelRequestDto createModelRequestDto)
        {
            var model = mapper.Map<CarsModel>(createModelRequestDto);
            await unitofWork.modelRepository.Create(model);
            await unitofWork.Commit();
        }

        public async Task DeleteModel(int id)
        {
            var model = await unitofWork.modelRepository.GetById(id);
            if (model == null)
                throw new KeyNotFoundException("Model not found");

            unitofWork.modelRepository.Delete(model);
            await unitofWork.Commit();
        }
        public async Task UpdateModel(UpdateModelRequestDto updateModelRequestDto)
        {
            var model = await unitofWork.modelRepository.GetById(updateModelRequestDto.Id);
            if (model == null)
                throw new Exception("Model not found");
            mapper.Map(updateModelRequestDto, model);
            unitofWork.modelRepository.Update(model);
            await unitofWork.Commit();
        }
    }
}
