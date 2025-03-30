using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Features
{
    public class GetFeatureDto:BaseEntity
    {
        public string FeatureName { get; set; }
        public int CarId {  get; set; }
    }
}
