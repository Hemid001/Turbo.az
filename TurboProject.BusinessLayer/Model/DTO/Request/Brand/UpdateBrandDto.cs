using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Request.Brand
{
    public class UpdateBrandDto:BaseEntity
    {
        public string Name { get; set; }
    }
}
