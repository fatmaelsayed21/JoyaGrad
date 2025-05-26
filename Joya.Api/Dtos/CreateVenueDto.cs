using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Joya.Api.Dtos
{
    public class CreateVenueDto
    {
        [Required]
        [StringLength(100)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(50)]
        public VenueType VenueType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateOnly Calendar { get; set; }

        public ICollection<CustomerReview>? CustomerReviews { get; set; }


        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        [Required]
        public string Location { get; set; }

        public double Rating { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string SellerId { get; set; }
    }
}
