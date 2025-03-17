using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Presistance.Repositries.Departments;
using IKEA.DAL.Presistance.Repositries.Employees;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;  }
        public IDepartmentRepository DepartmentRepository { get; }
        int Complete(); 
    }
}
