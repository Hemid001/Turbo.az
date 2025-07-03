using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Payment;
using TurboProject.BusinessLayer.Model.DTO.Response.Payment;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService paymentService;

        public PaymentController(PaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] CreatePaymentDto dto)
        {
            var response = new ApiResponse<string>();
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                response.Error("Unauthorized");
                return Unauthorized(response);
            }
            await paymentService.ProcessPaymentAsync(userId, dto);
            response.Success("Payment processed successfully.");
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetUserPayments()
        {
            var response = new ApiResponse<List<GetPaymentDto>>();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                response.Error("Unauthorized");
                return Unauthorized(response);
            }

            var payments = await paymentService.GetUserPaymentsAsync(userId);
            if (payments == null || !payments.Any())
            {
                response.Error("No payments found.");
                return NotFound(response);
            }

            response.Success(payments);
            return Ok(response);
        }
    }
}
