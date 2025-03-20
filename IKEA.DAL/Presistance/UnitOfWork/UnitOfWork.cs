using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositries.Departments;
using IKEA.DAL.Presistance.Repositries.Employees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return new EmployeeRepository(_dbContext);
            } 
        }
        public IDepartmentRepository DepartmentRepository 
        { 
            get
            {
                return new DepartmentRepository(_dbContext);
            }
        }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
