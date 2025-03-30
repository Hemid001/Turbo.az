using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.DataLayer.Entity
{
    public class EngineSize:BaseEntity
    {
        public decimal Engine { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
