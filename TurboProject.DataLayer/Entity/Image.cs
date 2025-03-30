namespace TurboProject.DataLayer.Entity
{
    public class Image : BaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string ImageURL { get; set; }
        public bool IsPrimary { get; set; }
    }
}
