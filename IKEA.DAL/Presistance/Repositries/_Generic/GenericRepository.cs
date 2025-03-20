using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistance.Repositries._Generic
{
    public class GenericRepository<T>:IGenericRepository<T> where T:ModelBase
    {
        private protected readonly ApplicationDbContext _DbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return await _DbContext.Set<T>().Where(X=>!X.IsDeleted).AsNoTracking().ToListAsync();
            }
            return await _DbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            //var T = _DbContext.Ts.Local.FirstOrDefault(D=>D.Id == id);
            //return T;
            return await _DbContext.Set<T>().FindAsync(id);
        }
        public IQueryable<T> GetAllAsQuerable()
        {
            return _DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _DbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _DbContext.Set<T>().Update(entity);
        }
    }
}
