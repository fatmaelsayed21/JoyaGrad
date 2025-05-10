using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class JoyaDbContext : IdentityDbContext<User>
    {
        public JoyaDbContext(DbContextOptions<JoyaDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Venue> Venues { get; set; }
       
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Decoration> Decorations { get; set; }
        public DbSet<PhotographyAndVideography> PhotographyAndVideographies { get; set; }
        public DbSet<MusicEnvironment> MusicEnvironments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }  

        public DbSet<Post> Posts { get; set; }

    }
}
