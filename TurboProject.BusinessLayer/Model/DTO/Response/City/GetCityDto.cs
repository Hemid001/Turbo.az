using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.City
{
    public class GetCityDto:BaseEntity
    {
        public string CityName { get; set; }
    }
}
