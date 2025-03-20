using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Departments
{
    public class CreatedDepartmentDto
    {

        [Required(ErrorMessage ="Code Is Required!!!!!")]
        public string Code { get; set; } = null!; //to make it don't accept null
        [Required(ErrorMessage = "Name Is Required!!!!!")]
        public string Name { get; set; } = null!; //to make it don't accept null 
        public string? Description { get; set; }
        [Display(Name ="Creation Date")]
        [Required(ErrorMessage = "Creation Date Is Required!!!!!")]
        public DateOnly CreationDate { get; set; }
    }
}
