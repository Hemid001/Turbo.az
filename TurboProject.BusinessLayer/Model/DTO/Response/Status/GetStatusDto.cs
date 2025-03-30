using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Status
{
    public class GetStatusDto : BaseEntity
    {
        public string StatusName { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
    }
}
