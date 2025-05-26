using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public enum DecorationType
    {
        Entrance = 1,
        Kosha = 2,
        Seating = 3,
        Flooring = 4,
        Other = 5

    }
    public class Decoration
    {
        [Key]
        public int DecorationId { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public DecorationType? DecorationType { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public DateOnly Calender { get; set; }

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


        public string Occaison { get; set; }
        [NotMapped]
        public int TotalBookings => Bookings?.Count ?? 0;

        public string SellerId { get; set; }
        public User Seller { get; set; } //navigational property
    }
}
