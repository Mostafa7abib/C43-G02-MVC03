using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositries._Generic;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistance.Repositries.Employees
{
    public class EmployeeRepository :GenericRepository<Employee> ,IEmployeeRepository
    {
       public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext) 
       {
            //Ask CLR for object from ApplicationDbContext implicitly
       }
    }
}
