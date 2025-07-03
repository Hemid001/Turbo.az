using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Mapper;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Impl;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.BusinessLayer
{
    public static class BusinessLayerConfig
    {
        public static void AddBusinessLayerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:ValidIssuer"],
                    ValidAudience = configuration["JwtSettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role
                };

            });
            var serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            SeedRolesAsync(serviceProvider).GetAwaiter().GetResult();
            SeedAdminAsync(serviceProvider, userManager, roleManager).GetAwaiter().GetResult();

            services.AddAutoMapper(typeof(IMapperRoute));
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarsModelService,CarsModelService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IFuelTypeService, FuelTypeService>();
            services.AddScoped<IEngineSizeService, EngineSizeService>();
            services.AddScoped<ITransmissionService, TransmissionService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IBodyTypeService, BodyTypeService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IStatusService,StatusService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<ITokenService, TokenService>();


        }
        public static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            string[] roles = { "Admin", "User", "Manager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new Role
                    {
                        Id = Guid.NewGuid().ToString(), 
                        Name = role
                    };

                    await roleManager.CreateAsync(newRole);
                }
            }
        }

        public static async Task SeedAdminAsync(this IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var role = await roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                role = new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);
            }

            var user = await userManager.FindByEmailAsync("hemid@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Hemid",
                    Surname = "Ismayilov",
                    Email = "hemid@gmail.com",
                    UserName= "hemid@gmail.com"
                };

                var result = await userManager.CreateAsync(user, "Hemid001");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");

                }
            }
        }
    }
}
