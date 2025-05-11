using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class ForgetPasswordDto
    {
        
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        
    }
}
