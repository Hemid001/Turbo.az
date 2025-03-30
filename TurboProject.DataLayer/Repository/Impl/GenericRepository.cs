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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<TEntity> table;

        public GenericRepository( AppDbContext context)
        {
            this.context = context;
            table= context.Set<TEntity>();
        }
        public async Task Create(TEntity entity)
        {
            await table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            table.Remove(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            table.Update(entity);
        }
    }
}
