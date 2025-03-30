namespace TurboProject.DataLayer.Entity
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CarsModel> Models { get; set; }
    }
}
