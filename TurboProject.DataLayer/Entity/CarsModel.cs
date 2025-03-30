using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.DataLayer.Entity
{
    public class CarsModel :BaseEntity
    {
        public int BrandId { get; set; }
        public Brand Brand  { get; set; }
        public string Name { get; set; }
    }
}
