using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public enum MusicEnvironmentType
    {
        DJ = 1,
        Singer =2,
        Zaffa = 3,
        Tabla = 4,
        Other = 5
    }
    public class MusicEnvironment
    {
        [Key]
        public int MusicEnvironmentId { get; set; }

        public string ImageUrl { get; set; }
        public string Location { get; set; }

        
        public MusicEnvironmentType? MusicEnvironmentType { get; set; }

       
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }

        
        public DateOnly Calendar { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Program number must be greater than 0")]
        public int ProgramNumber { get; set; }

        public ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public double Rating
        {
            get
            {
                if (CustomerReviews == null || !CustomerReviews.Any())
                    return 0;

                return Math.Round(CustomerReviews.Average(r => r.Rating), 1); 
            }
        }
        public string Occasion { get; set; }

        [NotMapped]
        public int TotalBookings => Bookings?.Count ?? 0;

        [Required]
        public string SellerId { get; set; }
        public User Seller { get; set; }
    }
}
