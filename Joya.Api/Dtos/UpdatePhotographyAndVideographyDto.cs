using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class UpdatePhotographyAndVideographyDto : CreatePhotographyAndVideographyDto
    {
        [Required]
        public int PhotoGraphy_VideoGraphyID { get; set; }

    }
}
