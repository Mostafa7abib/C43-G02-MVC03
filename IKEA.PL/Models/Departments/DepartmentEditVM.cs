using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models.Departments
{
    public class DepartmentEditVM
    {
        [Required(ErrorMessage ="Code Is Required!!!!!")]
        public string Code { get; set; } = null!; //to make it don't accept null
        [Required(ErrorMessage = "Name Is Required!!!!!")]
        public string Name { get; set; } = null!; //to make it don't accept null
        public string? Description { get; set; }
        [Required(ErrorMessage = "CreationDate Is Required!!!!!")]
        public DateOnly CreationDate { get; set; }
        public int Id { get; set; }
    }
}
