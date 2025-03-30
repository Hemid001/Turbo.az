using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DomainLayer.UoW.Impl;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.DomainLayer
{
    public static class DomainLayerConfig
    {
        public static void AddDomainLayerConfig(this IServiceCollection services)
        {
            services.AddScoped<IUnitofWork, UnitofWork>();
        }
    }
}   
