using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.CarsModel;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Impl;
using TurboProject.DomainLayer.UoW.Impl;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class BrandService:IBrandService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public BrandService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async Task CreateBrand(CreateBrandDto createBrandDto)
        {
            if (await unitofWork.brandRepository.ExistAsync(createBrandDto.Name))
                throw new Exception("Brand with this name already exists");
            var brand = mapper.Map<Brand>(createBrandDto);
            await unitofWork.brandRepository.Create(brand);
            await unitofWork.Commit();
        }

        public async Task DeleteBrand(int id)
        {
            var brand = await unitofWork.brandRepository.GetById(id);
            if (brand == null)
                throw new KeyNotFoundException("Brand not found");
            unitofWork.brandRepository.Delete(brand);
            await unitofWork.Commit();
        }

        public async Task<List<GetBrandDto>> GetAllBrands()
        {
            var brand = await unitofWork.brandRepository.GetAll();
            return mapper.Map<List<GetBrandDto>>(brand);
        }

        public async Task<GetBrandDto> GetBrandById(int id)
        {
            var brand = await unitofWork.brandRepository.GetById(id);
            return mapper.Map<GetBrandDto>(brand);
        }

        public async Task UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var brand = await unitofWork.brandRepository.GetById(updateBrandDto.Id);
            if (brand == null)
                throw new Exception("Brand not found");
            mapper.Map(updateBrandDto, brand);
            unitofWork.brandRepository.Update(brand);
            await unitofWork.Commit();
        }
    }
}
