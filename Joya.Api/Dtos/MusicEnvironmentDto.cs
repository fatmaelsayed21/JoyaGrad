using Domain.Models;

namespace Joya.Api.Dtos
{
    public class MusicEnvironmentDto
    {
        public int MusicEnvironmentId { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        public MusicEnvironmentType? MusicEnvironmentType { get; set; }

        public ICollection<CustomerReview>? CustomerReviews { get; set; }

        public int ProgramNumber { get; set; }
        public DateOnly Calendar { get; set; }
        public string Occasion { get; set; }
        public int TotalBookings { get; set; }
    }
}
