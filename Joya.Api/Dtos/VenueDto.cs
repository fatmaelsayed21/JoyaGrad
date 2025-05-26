using Domain.Models;

namespace Joya.Api.Dtos
{
    public class VenueDto
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }

        public int Capacity { get; set; }
        public string Description { get; set; }
        public DateOnly Calendar { get; set; }

        public string SellerId { get; set; }
        public VenueType? VenueType { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public ICollection<CustomerReview>? CustomerReviews { get; set; }

        public int TotalBooking { get; set; }
    }
}
