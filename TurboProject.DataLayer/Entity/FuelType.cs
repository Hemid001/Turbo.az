namespace TurboProject.DataLayer.Entity
{
    public class FuelType : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
