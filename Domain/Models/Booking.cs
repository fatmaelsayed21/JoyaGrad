using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Booking
    {
        public int BookingId { get; set; }

        // One-to-One relationship with Payment
        public Payment Payment { get; set; }

        [Required]
        public DateOnly EventDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be greater than or equal to 0")]
        public double TotalPrice { get; set; }

        // Optional service relationships
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public int? DecorationId { get; set; }
        public Decoration? Decoration { get; set; }

        public int? PhotographyAndVideographyId { get; set; }
        public PhotographyAndVideography? PhotographyAndVideography { get; set; }

        public int? MusicEnvironmentId { get; set; }
        public MusicEnvironment? MusicEnvironment { get; set; }

        [Required]
        public string BuyerId { get; set; }
        public User Buyer { get; set; }

        public bool HasAtLeastOneService => 
            VenueId.HasValue || 
            DecorationId.HasValue || 
            PhotographyAndVideographyId.HasValue || 
            MusicEnvironmentId.HasValue;
    }
}
