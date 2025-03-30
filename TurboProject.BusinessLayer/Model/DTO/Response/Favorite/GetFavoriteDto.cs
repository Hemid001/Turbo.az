using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Favorite
{
    public class GetFavoriteDto:BaseEntity
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
