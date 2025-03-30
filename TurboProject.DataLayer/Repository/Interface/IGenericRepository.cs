using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.DataLayer.Repository.Interface
{
    public interface IGenericRepository<TEntity>
    {
        Task Create(TEntity entity); 
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);

    }
}
