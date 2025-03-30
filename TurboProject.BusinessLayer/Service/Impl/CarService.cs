using AutoMapper;
using TurboProject.BusinessLayer.Model.DTO.Request.Car;
using TurboProject.BusinessLayer.Model.DTO.Response.Car;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Models.Car;
using TurboProject.DataLayer.Repository.Impl;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class CarService : ICarService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public CarService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async Task CreateCar(CreateCarRequestDto model)
        {
            var car = mapper.Map<Car>(model);
            await unitofWork.carRepository.Create(car);
            await unitofWork.Commit();
        }

        public async Task DeleteCar(int id)
        {
            var car = await unitofWork.carRepository.GetById(id);
            if (car != null)
            {
                unitofWork.carRepository.Delete(car);
                await unitofWork.Commit();      
            }
        }

        public async Task<List<GetCarResponseDto>> GetAllCars()
        {
            var cars = await unitofWork.carRepository.GetApprovedCars();
            return mapper.Map<List<GetCarResponseDto>>(cars);
        }

        public async Task<GetCarResponseDto> GetCarById(int id)
        {
            var car = await unitofWork.carRepository.GetById(id);
            return mapper.Map<GetCarResponseDto>(car);

        }

        public async Task<List<GetCarResponseDto>> GetFilteredCars(GetCarFilteredRequestDto filter)
        {
            var model = mapper.Map<GetCarFilteredRequestModel>(filter);

            var cars = await unitofWork.carRepository.GetFilteredCars(model);
            return mapper.Map<List<GetCarResponseDto>>(cars);
        }

        public async Task UpdateCar(UpdateCarRequestDto model)
        {
            var car = await unitofWork.carRepository.GetById(model.Id);
            if (car == null)
                throw new Exception("Car not found");
            mapper.Map(model,car);
            unitofWork.carRepository.Update(car);
            await unitofWork.Commit();
        }
    }
}
