using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.PL.Models.Departments;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            #region Employee

            #endregion

            #region Department
            CreateMap<DepartmentsDetailsReturnDto, DepartmentEditVM>().ReverseMap();
            CreateMap<DepartmentEditVM,UpdateDepartmentDto>().ReverseMap(); 
            CreateMap<CreatedDepartmentDto,DepartmentEditVM>().ReverseMap();
            #endregion
        }
    }
}
