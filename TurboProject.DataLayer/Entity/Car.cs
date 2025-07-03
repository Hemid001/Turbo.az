using TurboProject.DataLayer.Enum;

namespace TurboProject.DataLayer.Entity
{
    public class Car : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int ModelId { get; set; }
        public CarsModel Model { get; set; }
        public int EngineSizeId { get; set; }
        public EngineSize EngineSize { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        public int TransmissionId { get; set; }
        public Transmission  Transmission { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int Year { get; set; }
        public bool Barter { get; set; }
        public bool  Credit { get; set; }
        public string? VinCode { get; set; }
        public int SeatNumber { get; set; }
        public int Mileage { get; set; }
        public int HP { get; set; }
        public decimal Price { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public string Description { get; set; }
        public bool IsSold { get; set; }
        public bool IsApproved { get; set; } = false;
        public string? RejectionReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Favorite>  Favorites { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Feature> Features { get; set; }
    }
}
