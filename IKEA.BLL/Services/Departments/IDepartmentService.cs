using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;

namespace IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();
        Task<DepartmentsDetailsReturnDto?> GetDepartmentsByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartmetAsync(UpdateDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);  
    }
}
