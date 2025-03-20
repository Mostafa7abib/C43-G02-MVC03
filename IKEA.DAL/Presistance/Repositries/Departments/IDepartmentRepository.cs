using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositries._Generic;

namespace IKEA.DAL.Presistance.Repositries.Departments
{
    public  interface IDepartmentRepository:IGenericRepository<Department>
    {
        IEnumerable<Department> GetSpecificDepartment();
    }
}
