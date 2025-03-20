using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositries.Departments;
using IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsQuerable().Where(D => !D.IsDeleted)
                .Select(D => new DepartmentToReturnDto
                {
                    Id /*Of Dto*/ = D.Id /*Of DB*/,
                    Code = D.Code,
                    Name = D.Name,
                    //Description = D.Description,
                    CreationDate = D.CreationDate,
                }).AsNoTracking().ToListAsync();
            return departments; ;
        }

        public async Task<DepartmentsDetailsReturnDto?> GetDepartmentsByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
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
        
        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
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
              _unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateDepartmetAsync(UpdateDepartmentDto departmentDto)
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
             _unitOfWork.DepartmentRepository.Update(updatedDepartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is { })
            {
                  _unitOfWork.DepartmentRepository.Delete(department);
            }
            return await _unitOfWork.CompleteAsync()>0;
        }
    }
}
