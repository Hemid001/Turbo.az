using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Enum;

namespace TurboProject.BusinessLayer.Model.DTO.Request.Payment
{
    public class CreatePaymentDto
    {
        public int CarId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}

