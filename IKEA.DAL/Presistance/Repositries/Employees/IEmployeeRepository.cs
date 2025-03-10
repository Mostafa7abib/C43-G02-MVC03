using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositries._Generic;

namespace IKEA.DAL.Presistance.Repositries.Employees
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        
    }
}
