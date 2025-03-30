using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Enum;

namespace TurboProject.BusinessLayer.Model.DTO.Request.Car
{
    public class UpdateCarRequestDto : BaseEntity
    {
        public int? ModelId { get; set; }
        public int? EngineSizeId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? TransimissionId { get; set; }
        public int? CityId { get; set; }
        public int? Year { get; set; }
        public bool? Barter { get; set; }
        public bool? Credit { get; set; }
        public string? VinCode { get; set; }
        public int? SeatNumber { get; set; }
        public int? Mileage { get; set; }
        public int? HP { get; set; }
        public decimal? Price { get; set; }
        public CurrencyType? CurrencyType { get; set; }
        public string? Description { get; set; }
        public bool? IsSold { get; set; }
    }
}
