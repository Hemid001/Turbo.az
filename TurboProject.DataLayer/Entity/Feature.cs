using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.DataLayer.Entity
{
    public class Feature:BaseEntity
    {
        public string Name { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

    }
}
