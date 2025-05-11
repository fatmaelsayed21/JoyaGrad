using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is Required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required !!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)] ///******
        public string Password { get; set; }


    }
}
