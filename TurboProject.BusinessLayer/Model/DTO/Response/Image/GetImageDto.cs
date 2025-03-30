using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Image
{
    public class GetImageDto:BaseEntity
    {
        public string ImageURL { get; set; }
        public bool IsPrimary { get; set; }
    }
}
