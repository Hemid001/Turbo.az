using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.DataLayer.Repository.Interface
{
    public interface IPaymentRepository:IGenericRepository<Payment>
    {
        Task<List<Payment>> GetPaymentsByUserIdAsync(string userId);
    }
}
