using TurboProject.DataLayer.Enum;

namespace TurboProject.DataLayer.Entity
{
    public class Payment : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
