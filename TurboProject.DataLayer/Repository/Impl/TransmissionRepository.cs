using Microsoft.EntityFrameworkCore;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.DataLayer.Repository.Impl
{
    public class TransmissionRepository : GenericRepository<Transmission>, ITransmissionRepository
    {
        private readonly AppDbContext context;

        public TransmissionRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async  Task<bool> ExistsAsync(string name)
        {
            return await context.Transmissions.AnyAsync(ft => ft.TransmissionName == name);
        }
    }
}
