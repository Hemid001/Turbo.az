using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.DataLayer.Entity
{
    public class BodyType : BaseEntity
    {
        public string BodyTypeName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
