using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum ServiceType
    {
        Photography = 1,
        Videography = 2
    }

    public class PhotographyAndVideography
    {
        [Key]
        public int PhotoGraphy_VideoGraphyID { get; set; }


        public string ImageUrl { get; set; }

        [Required]
        public ServiceType Type { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }

        [Required]
        public DateOnly Calender { get; set; }

        public ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Program number must be greater than 0")]
        public int ProgramNumber { get; set; }


        public double Rating { get; set; }

        [NotMapped]
        public int TotalBookings => Bookings?.Count ?? 0;

        [Required]
        public string SellerId { get; set; }
        public User Seller { get; set; }
    }
}
