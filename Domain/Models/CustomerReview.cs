using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CustomerReview
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public double Rating { get; set; }

        public string? Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional service relationships
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public int? DecorationId { get; set; }
        public Decoration? Decoration { get; set; }

        public int? PhotographyAndVideographyId { get; set; }
        public PhotographyAndVideography? PhotographyAndVideography { get; set; }

        public int? MusicEnvironmentId { get; set; }
        public  MusicEnvironment? MusicEnvironment{ get; set; }

        // Reviewer (Buyer)
        [Required]
        public string BuyerId { get; set; }
        public User Buyer { get; set; }

        // Reviewed Seller
        [Required]
        public string SellerId { get; set; }
        public User Seller { get; set; }
    }
}
