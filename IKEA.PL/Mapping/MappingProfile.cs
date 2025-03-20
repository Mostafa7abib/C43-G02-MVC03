using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.PL.Models.Departments;
using IKEA.PL.Models.Employees;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            #region Employee
            CreateMap<EmployeeDetailsDto, EmployeeEditVM>().ReverseMap();
            CreateMap<EmployeeEditVM, UpdatedEmployeeDto>().ReverseMap();
            CreateMap<CreatedEmployeeDto, EmployeeEditVM>().ReverseMap();
            #endregion

            #region Department
            CreateMap<DepartmentsDetailsReturnDto, DepartmentEditVM>().ReverseMap();
            CreateMap<DepartmentEditVM,UpdateDepartmentDto>().ReverseMap(); 
            CreateMap<CreatedDepartmentDto,DepartmentEditVM>().ReverseMap();
            #endregion
        }
    }
}
