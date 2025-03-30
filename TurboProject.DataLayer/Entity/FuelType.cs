namespace TurboProject.DataLayer.Entity
{
    public class FuelType : BaseEntity
    {
        public string FuelTypeName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
