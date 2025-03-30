using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Transmission
{
    public class GetTransmissionTypeDto:BaseEntity
    {
        public string TransmissionName { get; set; }
    }
}
