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
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext context;

        public RefreshTokenRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(RefreshToken token)
        {
            await context.RefreshTokens.AddAsync(token);
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await context.RefreshTokens
                                 .Include(rt => rt.User)
                                 .SingleOrDefaultAsync(rt => rt.Token == token);

        }

        public void Update(RefreshToken token)
        {
            context.RefreshTokens.Update(token);
        }
    }
}
