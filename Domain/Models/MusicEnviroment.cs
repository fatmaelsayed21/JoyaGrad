using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public class MusicEnvironment
    {
        [Key]
        public int MusicEnvironmentId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string MusicEnvironmentType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }

        [Required]
        public DateOnly Calendar { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Program number must be greater than 0")]
        public int ProgramNumber { get; set; }

        public ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [Required]
        public string SellerId { get; set; }
        public User Seller { get; set; }
    }
}
