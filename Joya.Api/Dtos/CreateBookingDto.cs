using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class CreateBookingDto
    {
       
            [Required]
            public DateOnly EventDate { get; set; }

            public int? VenueId { get; set; }
            public int? DecorationId { get; set; }
            public int? PhotographyAndVideographyId { get; set; }
            public int? MusicEnvironmentId { get; set; }
        
    }
}
