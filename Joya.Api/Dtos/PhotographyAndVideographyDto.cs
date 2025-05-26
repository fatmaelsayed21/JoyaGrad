using Domain.Models;

namespace Joya.Api.Dtos
{
    public class PhotographyAndVideographyDto
    {
        public int Photography_VideographyID { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        public ICollection<CustomerReview>? CustomerReviews { get; set; }

        public int TotalBookings { get; set; }
    }
}
