using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class UpdateVenueDto : CreateVenueDto
    {
        [Required]
        public int VenueId { get; set; }

    }
}
