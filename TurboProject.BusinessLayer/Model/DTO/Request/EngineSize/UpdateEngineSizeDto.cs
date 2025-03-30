using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Request.EngineSize
{
    public class UpdateEngineSizeDto:BaseEntity
    {
        public decimal Engine { get; set; }
    }
}
