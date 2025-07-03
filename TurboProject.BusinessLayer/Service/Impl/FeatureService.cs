using AutoMapper;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Response.Features;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public FeatureService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async Task<List<GetFeatureDto>> GetFeaturesByCarId(int carId)
        {
            var features = await unitofWork.featureRepository.GetFeaturesByCarId(carId);
            return mapper.Map<List<GetFeatureDto>>(features);
        }

        public async Task AddFeature(CreateFeatureDto dto)
        {
            var feature = mapper.Map<Feature>(dto);
            await unitofWork.featureRepository.Create(feature);
            await unitofWork.Commit();

        }

        public async Task DeleteFeature(int id)
        {
            var feature = await unitofWork.featureRepository.GetById(id);
            if (feature != null)
            {
                unitofWork.featureRepository.Delete(feature);
            }
            await unitofWork.Commit();
        }
    }
}
