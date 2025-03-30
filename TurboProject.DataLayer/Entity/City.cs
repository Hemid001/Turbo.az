namespace TurboProject.DataLayer.Entity
{
    public class City : BaseEntity
    {
        public string CityName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
