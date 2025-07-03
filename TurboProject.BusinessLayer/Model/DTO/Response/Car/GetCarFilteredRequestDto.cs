namespace TurboProject.BusinessLayer.Model.DTO.Response.Car
{
    public class GetCarFilteredRequestDto
    {
        public string? Model { get; set; }
        public string? Category { get; set; }
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
