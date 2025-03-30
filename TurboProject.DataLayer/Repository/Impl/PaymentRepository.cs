using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer.Repository.Impl
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext context;

        public PaymentRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<List<Payment>> GetPaymentsByUserIdAsync(string userId)
        {
            return await context.Payments
                .Include(c=>c.Car)
                .Where(u=>u.UserId == userId)
                .ToListAsync(); 
        }
    }
}
