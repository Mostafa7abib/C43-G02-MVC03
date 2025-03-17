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
        public IEnumerable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return _DbContext.Set<T>().Where(X=>!X.IsDeleted).AsNoTracking().ToList();
            }
            return _DbContext.Set<T>().Where(X => !X.IsDeleted).ToList();
        }

        public T? GetById(int id)
        {
            //var T = _DbContext.Ts.Local.FirstOrDefault(D=>D.Id == id);
            //return T;
            return _DbContext.Set<T>().Find(id);
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
