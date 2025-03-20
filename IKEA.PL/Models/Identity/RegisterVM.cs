using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models.Identity
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="First Name Is Required")]
        public string FName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is Required")]
        public string LName { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; } = null!;

        public bool IsAgree { get; set; }

    }
}
