using System.Text.Json.Serialization;
using TurboProject.DataLayer.Enum;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Car
{
    public class GetCarResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Brand { get; set; }      
        public string Category { get; set; }   
        public string City { get; set; }      
        public int Year { get; set; }
        public int Price { get; set; }
        public bool Barter { get; set; }
        public bool Credit { get; set; }
        public string VinCode { get; set; }
        public int SeatNumber { get; set; }
        public int Mileage { get; set; }
        public int HP { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrencyType CurrencyType { get; set; }
        public string Description { get; set; }
        public bool IsSold { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
