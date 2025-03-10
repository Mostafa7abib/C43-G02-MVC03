using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositries._Generic;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistance.Repositries.Departments
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            //Ask CLR for object from ApplicationDbContext implicitly
        }

        public IEnumerable<Department> GetSpecificDepartment()
        {
            throw new NotImplementedException();
        }
    }
}
