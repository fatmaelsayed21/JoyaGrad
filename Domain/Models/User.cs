using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
   
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [NotMapped] 
        public string FullName => $"{FirstName} {LastName}";




        // Buyer relationships
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<CustomerReview> WrittenReviews { get; set; } = new List<CustomerReview>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // Seller relationships
        public ICollection<Venue> Venues { get; set; } = new List<Venue>();
        public ICollection<Decoration> Decorations { get; set; } = new List<Decoration>();
        public ICollection<PhotographyAndVideography> PhotographyAndVideographies { get; set; } = new List<PhotographyAndVideography>();
        public ICollection<MusicEnvironment> MusicEnviroments { get; set; } = new List<MusicEnvironment>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        



        
    }
}
