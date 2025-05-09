using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Venue 
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(50)]
        public string VenueType { get; set; }

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

        public ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();

        // Navigation properties for bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [Required]
        public string SellerId { get; set; }
        public User Seller { get; set; }
    }
}
