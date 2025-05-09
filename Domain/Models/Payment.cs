using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0")]
        public double Amount { get; set; }

        [Required]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        
        public DateTime PaymentDate { get; set; }

        [Required]
        public string BuyerId { get; set; }
        public User Buyer { get; set; }
    }
}
