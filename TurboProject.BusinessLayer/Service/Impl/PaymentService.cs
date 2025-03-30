using AutoMapper;
using TurboProject.BusinessLayer.Model.DTO.Request.Payment;
using TurboProject.BusinessLayer.Model.DTO.Response.Payment;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Impl;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public PaymentService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        public async Task<List<GetPaymentDto>> GetUserPaymentsAsync(string userId)
        {
           var payments = await unitofWork.paymentRepository.GetPaymentsByUserIdAsync(userId);
            return mapper.Map<List<GetPaymentDto>>(payments);
        }

        public async Task ProcessPaymentAsync(string userId, CreatePaymentDto dto)
        {
            var car = await unitofWork.carRepository.GetById(dto.CarId);
            if (car == null || car.UserId != userId)
            {
                throw new Exception("Car not found or you don't have permission to pay for this car.");
            }

            var payment = new Payment
            {
                UserId = userId,
                CarId = dto.CarId,
                Amount = dto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = dto.PaymentStatus
            };

            await unitofWork.paymentRepository.Create(payment);
            await unitofWork.Commit();
        }
    }
    }

