using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Employees;

namespace IKEA.DAL.Models.Departments
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!; //to make it don't accept null 
        public string Code { get; set; } = null!; //to make it don't accept null
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
        #region Employees
        //Navigational Property [many]
        public virtual ICollection<Employee> Employees { get; set; }= new HashSet<Employee>();
        #endregion
    }
}
