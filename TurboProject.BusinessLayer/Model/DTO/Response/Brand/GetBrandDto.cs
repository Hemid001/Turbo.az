using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Brand
{
    public class GetBrandDto:BaseEntity
    {
        public string Name { get; set; }
    }
}
