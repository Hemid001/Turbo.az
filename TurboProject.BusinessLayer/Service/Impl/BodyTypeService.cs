using AutoMapper;
using TurboProject.BusinessLayer.Model.DTO.Request.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Response.BodyType;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public BodyTypeService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<List<GetBodyTypeDto>> GetAllTypes()
        {
            var type = await unitofWork.bodyTypeRepository.GetAll();
            return mapper.Map<List<GetBodyTypeDto>>(type);
        }

        public async Task<GetBodyTypeDto> GetById(int id)
        {
            var type = await unitofWork.bodyTypeRepository.GetById(id);
            return mapper.Map<GetBodyTypeDto>(type);
        }
        public async Task CreateBodyType(CreateBodyTypeDto dto)
        {
            if (await unitofWork.bodyTypeRepository.ExistsAsync(dto.Name))
                throw new Exception("Body type  with this name already exists");
            var type = mapper.Map<BodyType>(dto);
            await unitofWork.bodyTypeRepository.Create(type);
            await unitofWork.Commit();
        }

        public async Task DeleteBodyType(int id)
        {
            var type = await unitofWork.bodyTypeRepository.GetById(id);
            if (type == null)
                throw new KeyNotFoundException("Body type not found");
            unitofWork.bodyTypeRepository.Delete(type);
            await unitofWork.Commit();
        }
    }
}
