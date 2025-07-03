using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.BusinessLayer.Model.DTO.Request.Features
{
    public class CreateFeatureDto
    {
        public string Name { get; set; }
        public int CarId { get; set; }
    }
}
