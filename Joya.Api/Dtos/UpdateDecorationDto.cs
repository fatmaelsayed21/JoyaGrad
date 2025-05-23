using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class UpdateDecorationDto : CreateDecorationDto
    {
        [Required]
        public int DecorationId { get; set; }
    }
}
