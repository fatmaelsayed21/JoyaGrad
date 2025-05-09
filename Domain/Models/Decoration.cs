using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Decoration
    {
        [Key]
        public int DecorationID { get; set; }
        public string Location { get; set; }
        public string DecorationType { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public DateOnly Calender { get; set; }

        public int ProgramNumber { get; set; }
        public ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public string SellerId { get; set; }
        public User Seller { get; set; } //navigational property
    }
}
