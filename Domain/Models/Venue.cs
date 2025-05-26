using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum VenueType
    {
        Hall = 1,
        Beach = 2,
        Graden = 3,
        RoofTop = 4,
        Yacht = 5 
        

    }
    public class Venue 
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string VenueName { get; set; }

        
        [StringLength(50)]
        public VenueType? VenueType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateOnly Calendar { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [Required]
        public string Location { get; set; }

        public double Rating
        {
            get
            {
                if (CustomerReviews == null || !CustomerReviews.Any())
                    return 0;

                return Math.Round(CustomerReviews.Average(r => r.Rating), 1);
            }
        }

        public string ImageUrl { get; set; }

        public ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();

        // Navigation properties for bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();


        public string Occasion { get; set; }


        [NotMapped]
        public int TotalBookings => Bookings?.Count ?? 0;

        [Required]
        public string SellerId { get; set; }
        public User Seller { get; set; }
    }
}
