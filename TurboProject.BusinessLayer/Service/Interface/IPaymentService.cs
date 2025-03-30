using TurboProject.BusinessLayer.Model.DTO.Request.Payment;
using TurboProject.BusinessLayer.Model.DTO.Response.Payment;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(string userId, CreatePaymentDto dto);
        Task<List<GetPaymentDto>> GetUserPaymentsAsync(string userId);
    }
}
