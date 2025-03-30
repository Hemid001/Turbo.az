using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.DataLayer.Models.Car
{
    public class GetCarFilteredRequestModel
    {
        public string Model { get; set; }
        public string Category { get; set; }
        public string? City { get; set; }
        public string? FuelType { get; set; }
        public string? Transmission { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? CurrencyType { get; set; }
        public int? MinHP { get; set; }
        public int? MaxHP { get; set; }
    }
}
