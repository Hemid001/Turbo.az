using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Impl;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer
{
    public static class DataAccessLayerConfig
    {
        public static void AddDataAccessLayerConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("Default"));
            });


            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>() 
            .AddDefaultTokenProviders();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IEngineSizeRepository, EngineSizeRepository>();
            services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
            services.AddScoped<ITransmissionRepository, TransmissionRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IBodyTypeRepository, BodyTypeRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();


        }
    }
}
