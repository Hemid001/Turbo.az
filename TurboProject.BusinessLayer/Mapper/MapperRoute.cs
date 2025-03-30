
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Account;
using TurboProject.BusinessLayer.Model.DTO.Request.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.Car;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Request.City;
using TurboProject.BusinessLayer.Model.DTO.Request.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Request.Favorite;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Status;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.Car;
using TurboProject.BusinessLayer.Model.DTO.Response.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Response.City;
using TurboProject.BusinessLayer.Model.DTO.Response.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Response.Favorite;
using TurboProject.BusinessLayer.Model.DTO.Response.Features;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.Image;
using TurboProject.BusinessLayer.Model.DTO.Response.Payment;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;
using TurboProject.BusinessLayer.Model.DTO.Response.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.User;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Models.Car;

namespace TurboProject.BusinessLayer.Mapper
{
    public class MapperRoute : Profile
    {
        public MapperRoute()
        {
            CreateMap<RegisterRequestDto, User>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // UserName = Email
    


            CreateMap<CreateCarRequestDto, Car>().ReverseMap();

            CreateMap<GetCarResponseDto, Car>().ReverseMap();

            CreateMap<UpdateCarRequestDto, Car>().ReverseMap()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateModelRequestDto, CarsModel>().ReverseMap();
            CreateMap<GetModelResponseDto, CarsModel>().ReverseMap();
            CreateMap<UpdateModelRequestDto, CarsModel>().ReverseMap();
            CreateMap<UpdateBrandDto, Brand>().ReverseMap();
            CreateMap<GetBrandDto, Brand>().ReverseMap();
            CreateMap<CreateBrandDto, Brand>().ReverseMap();
            CreateMap<UpdateEngineSizeDto, EngineSize>().ReverseMap();
            CreateMap<CreateEngineSizeDto, EngineSize>().ReverseMap();
            CreateMap<EngineSize, GetEngineSizeDto>().ReverseMap();
            CreateMap<CreateFuelTypeDto, FuelType>().ReverseMap();
            CreateMap<GetFuelTypeDto, FuelType>().ReverseMap();
            CreateMap<CreateTransmissionTypeDto, Transmission>().ReverseMap();
            CreateMap<GetTransmissionTypeDto, Transmission>().ReverseMap();
            CreateMap<GetCityDto, City>().ReverseMap();
            CreateMap<CreateCityDto, City>().ReverseMap();
            CreateMap<GetCarFilteredRequestModel, GetCarFilteredRequestDto>().ReverseMap();
            CreateMap<GetImageDto, Image>().ReverseMap();
            CreateMap<BodyType, CreateBodyTypeDto>().ReverseMap();
            CreateMap<BodyType, GetBodyTypeDto>().ReverseMap();
            CreateMap<Favorite, GetFavoriteDto>()
    .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => src.Car.Model.Name)).ReverseMap();
            CreateMap<CreateFavoriteDto, Favorite>()
           .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<Payment, GetPaymentDto>()
            .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => src.Car.Model.Name))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()));
            CreateMap<CreateStatusDto, Status>().ReverseMap();
            CreateMap<UpdateStatusDto, Status>().ReverseMap();
            CreateMap<GetStatusDto, Status>().ReverseMap();
            CreateMap<Feature, GetFeatureDto>().ReverseMap();
            CreateMap<CreateFeatureDto, Feature>().ReverseMap();
            CreateMap<User, GetUserDto>();
               



        }
    }
}
