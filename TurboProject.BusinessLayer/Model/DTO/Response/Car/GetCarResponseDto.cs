using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Enum;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Car
{
    public class GetCarResponseDto
    {
        public class CarResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Category { get; set; }
            public int Year { get; set; }
            public int Price { get; set; }
            public bool Barter { get; set; }
            public bool Credit { get; set; }
            public string VinCode { get; set; }
            public int SeatNumber { get; set; }
            public int Mileage { get; set; }
            public int HP { get; set; }
            public CurrencyType CurrencyType { get; set; }
            public string Description { get; set; }
            public bool IsSold { get; set; }
            public DateTime CreatedAt { get; set; }
        }

    }
}
