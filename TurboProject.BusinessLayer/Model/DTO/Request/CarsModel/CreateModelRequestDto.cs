using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.BusinessLayer.Model.DTO.Request.CarsModel
{
    public class CreateModelRequestDto
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
    }
}
