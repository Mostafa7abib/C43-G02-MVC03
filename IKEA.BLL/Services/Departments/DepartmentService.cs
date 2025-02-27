using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositries.Departments;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Departments
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepositoryRebo;

        public DepartmentService(IDepartmentRepository departmentRepositoryRebo)
        {
            _departmentRepositoryRebo = departmentRepositoryRebo;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepositoryRebo.GetAllAsQuerable()
                .Select(D => new DepartmentToReturnDto
                {
                    Id /*Of Dto*/ = D.Id /*Of DB*/,
                    Code = D.Code,
                    Name = D.Name,
                    Description = D.Description,
                    CreationDate = D.CreationDate,
                }).AsNoTracking().ToList();
            return departments; ;
        }

        public DepartmentsDetailsReturnDto? GetDepartmentsById(int id)
        {
            var department = _departmentRepositoryRebo.GetById(id);
            if (department is { }) // {} here = is not null
            {
                return new DepartmentsDetailsReturnDto()
                {
                    Id /*Of Dto*/ = department.Id /*Of DB*/,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModificationBy = department.LastModificationBy,
                    LastModificationOn = department.LastModificationOn,
                };
            }
            return null;
        }
        
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Department
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModificationBy= 1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _departmentRepositoryRebo.Add(CreatedDepartment);
        }

        public int UpdateDepartmet(UpdateDepartmentDto departmentDto)
        {
            var updatedDepartment = new Department()
            {
                Id= departmentDto.Id ,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModificationBy = 1 , 
                LastModificationOn = DateTime.UtcNow,  
            };
            return _departmentRepositoryRebo.Update(updatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepositoryRebo.GetById(id);
            if (department is { })
            {
                return _departmentRepositoryRebo.Delete(department)>0;
            }
            return false;
        }
    }
}
